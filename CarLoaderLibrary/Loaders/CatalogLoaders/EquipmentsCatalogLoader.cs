using CarCatalogLibrary;
using CarCatalogLibrary.ResponseModels.Colors;
using CarCatalogLibrary.ResponseModels.Equipments;
using CarCatalogLibrary.ResponseModels.Feature;
using CarDBLibrary.DataAccess;
using CarDBLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarLoaderLibrary.Loaders.CatalogLoaders
{
    public class EquipmentsCatalogLoader : BaseCatalogLoader
    {
        public EquipmentsCatalogLoader(CarContext carContext,
                                 CarCatalogLibrary.Client ilsaClient
           ) : base(carContext, ilsaClient) { }
        public async Task<EquipmentCatalog> FindOrCreateEquipmentCatalogAsync(int databaseId)
        {
            return await this.CarContext.FindOrCreateEquipmentCatalogAsync(databaseId);
        }
        public async Task<EquipmentCatalog> FindOrCreateEquipmentCatalogAsync(EquipmentsEdge edge)
        {
            var databaseId = edge.node.databaseId;
            return await this.FindOrCreateEquipmentCatalogAsync(databaseId);
        }
        public override async Task LoadAsync()
        {
            var lastEquipmentTask = this.CarContext.EquipmentTask.OrderByDescending(x => x.CreatedOn).Take(1).SingleOrDefault();

            DateTime? since = null;
            var after = "";
            if (lastEquipmentTask != null)
            {
                if (lastEquipmentTask.Result == EquipmentTask.ResultEquipmentTaskStep.Error)
                {
                    since = lastEquipmentTask.Since;
                    after = lastEquipmentTask.Cursor;
                }
                else
                {
                    since = lastEquipmentTask.NextSince;
                }
                
            }
            var equipmentTask = new EquipmentTask() 
            {
                Since = since
            };
            this.CarContext.EquipmentTask.Add(equipmentTask);
            this.CarContext.SaveChanges();
            Task<ResultLoader> addEquipments = null;
            try
            {
                await foreach (var equipments in this.IlsaClient.GetAllEquipmentsAsync(5, after, since))
                {

                    if (addEquipments != null)
                    {
                        await addEquipments;
                        if (addEquipments.Result.Status == StatusResultLoader.Error)
                        {
                            equipmentTask.Result = EquipmentTask.ResultEquipmentTaskStep.Error;
                            this.CarContext.SaveChanges();
                            throw addEquipments.Result.Exception;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(addEquipments.Result.Cursor))
                            {
                                throw new ArgumentNullException("Не вернулся курсок");
                            }
                            equipmentTask.Cursor = addEquipments.Result.Cursor;
                            after = addEquipments.Result.Cursor;
                            this.CarContext.SaveChanges();
                        }
                        
                    }
                    if (addEquipments != null && addEquipments.IsFaulted)
                    {
                        throw addEquipments.Exception;
                    }
                    addEquipments = Task.Run(() => UpdateEquipmentsAsync(equipments.edges));
                    

                }
                if (addEquipments != null)
                {
                    await addEquipments;
                }
                if (addEquipments != null && addEquipments.IsFaulted)
                {
                    throw addEquipments.Exception;
                }

                equipmentTask.Result = EquipmentTask.ResultEquipmentTaskStep.Success;
                equipmentTask.NextSince = DateTime.Now;
                this.CarContext.SaveChanges();
            }
            catch (Exception)
            {
                equipmentTask.Result = EquipmentTask.ResultEquipmentTaskStep.Error;
                this.CarContext.SaveChanges();
                
                throw;
            }
            
           
        }
        private async Task<ResultLoader> UpdateEquipmentsAsync(IEnumerable<EquipmentsEdge> edges)
        {
            var result = new ResultLoader 
            {
                Cursor = ""
            };
            try
            {
                List<Task> tasks = null;
                foreach (var edge in edges)
                {
                    
                    if (tasks != null)
                    {
                        Task.WaitAll(tasks.ToArray());
                    }
                    tasks = new List<Task>();

                    var equipmentCatalog = 
                        await this.CarContext.FindOrCreateEquipmentCatalogAsync
                        (
                            edge.node.databaseId
                        );

                    tasks.AddRange
                        (
                            new Task[]
                            {
                            UpdateVersionEquipment(equipmentCatalog, edge),
                            UpdateSpecifications(equipmentCatalog, edge),
                            UpdatePricelistsEquipment(equipmentCatalog, edge)
                            }
                        );
                    result.Cursor = edge.cursor;
                }
                result.Status = StatusResultLoader.Success;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Status = StatusResultLoader.Error;
            }
            return result;


        }
        private async Task UpdateVersionEquipment(
            EquipmentCatalog equipmentCatalog,
            EquipmentsEdge edge)
        {
            var versionCatalog = await this.VersionsCatalogLoader.FindOrCreateVersionCatalogAsync(edge);
            await this.CarContext.FindOrCreateEquipmentVersionCatalogAsync(
                equipmentCatalog.Id,
                versionCatalog.Id
                );
        }
        private async Task UpdatePricelistsEquipment(
            EquipmentCatalog equipmentCatalog,
            EquipmentsEdge edge)
        {
            foreach (var color in edge.node.colors)
            {
                await UpdatePricelistEquipment(equipmentCatalog, color);
            }
        }
        private async Task UpdatePricelistEquipment(
           EquipmentCatalog equipmentCatalog,
           ColorNode colorNode)
        {
            string pricelist = null;
            DateTime? productionFromDate = null;
            DateTime? validFromDate = null;
            if (colorNode.pricelist != null)
            {
                pricelist = colorNode.pricelist.price;
                productionFromDate = colorNode.pricelist.productionFromDate;
                validFromDate = colorNode.pricelist.validFromDate;
            }
            await this.CarContext.FindOrCreatePricelistCatalogAsync
                (
                    colorNode.name,
                    colorNode.metallic,
                    colorNode.hue,
                    colorNode.picture,
                    pricelist,
                    productionFromDate,
                    validFromDate,
                    equipmentCatalog.Id
                );
        }
        private async Task UpdateSpecifications(
            EquipmentCatalog equipmentCatalog,
            EquipmentsEdge edge) 
        {
            if (edge.node.specification != null)
            {
                if (edge.node.specification.description != null)
                {
                    foreach (var description in edge.node.specification.description)
                    {
                        await this.UpdateSpecification
                            (
                            equipmentCatalog.Id,
                            description.name,
                            description.features
                            );
                    }
                }

                if (edge.node.specification.technical != null)
                {
                    await this.UpdateSpecification
                  (
                  equipmentCatalog.Id,
                  "Техническое описание",
                  edge.node.specification.technical
                  );
                }
            }
           
           
            await this.SpecificationsCatalogLoader.ActivateNewSpecificationCatalogAsync(equipmentCatalog.Id);
        }
        private async Task UpdateSpecification(
            Guid equipmentId, 
            string descriptionName, 
            List<FeatureNode> featureNodes)
        {
            var description = await this.DescriptionsCatalogLoader.FindOrCreateDescriptionCatalogAsync
                (
                descriptionName
                );

            foreach (var featureNode in featureNodes)
            {
                var feature = await this.FeaturesCatalogLoader.FindOrCreateEquipmentCatalogAsync(description.Id, featureNode);
                var specification = await this.SpecificationsCatalogLoader.FindOrCreateSpecificationCatalogAsync
                    (
                    equipmentId,
                    feature.Id
                    );
                if (featureNode.ins != null)
                {
                    foreach (var _in in featureNode.ins)
                    {
                        var inclusion = await this.InclusionCatalogLoader.FindOrCreateEquipmentCatalogAsync(_in);
                        var specificationInclusion = this.SpecificationInclusionsCatalogLoader.FindOrCreateSpecificationInclusionCatalogAsync(
                            inclusion.Id,
                            specification.Id
                            );
                    }
                }
               
            }
            
        }
    }
}
