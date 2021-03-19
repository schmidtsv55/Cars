using CarDBLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarLoaderLibrary.Loaders.CatalogLoaders
{
    public abstract class BaseCatalogLoader : ILoader
    {
        private VersionCatalogLoader _versionsCatalogLoader;
        internal VersionCatalogLoader VersionsCatalogLoader 
        {
            get 
            {
                if (this._versionsCatalogLoader == null)
                {
                    this._versionsCatalogLoader = new VersionCatalogLoader(this.CarContext, this.IlsaClient);
                }
                return this._versionsCatalogLoader;
            }
        }

        private DescriptionsCatalogLoader _descriptionsCatalogLoader;
        internal DescriptionsCatalogLoader DescriptionsCatalogLoader
        {
            get
            {
                if (this._descriptionsCatalogLoader == null)
                {
                    this._descriptionsCatalogLoader = new DescriptionsCatalogLoader(this.CarContext, this.IlsaClient);
                }
                return this._descriptionsCatalogLoader;
            }
        }
        private ModificationsCatalogLoader _modificationsCatalogLoader;
        internal ModificationsCatalogLoader ModificationsCatalogLoader
        {
            get
            {
                if (this._modificationsCatalogLoader == null)
                {
                    this._modificationsCatalogLoader = new ModificationsCatalogLoader(this.CarContext, this.IlsaClient);
                }
                return this._modificationsCatalogLoader;
            }
        }

        private FeaturesCatalogLoader _featuresCatalogLoader;
        internal FeaturesCatalogLoader FeaturesCatalogLoader
        {
            get
            {
                if (this._featuresCatalogLoader == null)
                {
                    this._featuresCatalogLoader = new FeaturesCatalogLoader(this.CarContext, this.IlsaClient);
                }
                return this._featuresCatalogLoader;
            }
        }

        private SpecificationCatalogLoader _specificationsCatalogLoader;
        internal SpecificationCatalogLoader SpecificationsCatalogLoader
        {
            get
            {
                if (this._specificationsCatalogLoader == null)
                {
                    this._specificationsCatalogLoader = new SpecificationCatalogLoader(this.CarContext, this.IlsaClient);
                }
                return this._specificationsCatalogLoader;
            }
        }

        private InclusionCatalogLoader _inclusionCatalogLoader;
        internal InclusionCatalogLoader InclusionCatalogLoader
        {
            get
            {
                if (this._inclusionCatalogLoader == null)
                {
                    this._inclusionCatalogLoader = new InclusionCatalogLoader(this.CarContext, this.IlsaClient);
                }
                return this._inclusionCatalogLoader;
            }
        }

        private SpecificationInclusionsCatalogLoader _specificationInclusionsCatalogLoader;
        internal SpecificationInclusionsCatalogLoader SpecificationInclusionsCatalogLoader
        {
            get
            {
                if (this._specificationInclusionsCatalogLoader == null)
                {
                    this._specificationInclusionsCatalogLoader = new SpecificationInclusionsCatalogLoader(this.CarContext, this.IlsaClient);
                }
                return this._specificationInclusionsCatalogLoader;
            }
        }

        internal CarContext CarContext;
        internal CarCatalogLibrary.Client IlsaClient;
        internal DateTime? Since;
        public BaseCatalogLoader(CarContext carContext, 
                                 CarCatalogLibrary.Client ilsaClient
            ) : this(carContext, ilsaClient, null)
        {
           
        }
        public BaseCatalogLoader(CarContext carContext, 
                                 CarCatalogLibrary.Client ilsaClient, 
                                 DateTime? since)
        {
            this.CarContext = carContext;
            this.IlsaClient = ilsaClient;
            this.Since = since;
        }
        virtual public async Task LoadAsync() { }
    }
}
