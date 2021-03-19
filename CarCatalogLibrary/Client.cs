using CarCatalogLibrary.ResponseModels.Equipments;
using CarCatalogLibrary.ResponseModels.Makes;
using CarCatalogLibrary.ResponseModels.Models;
using CarCatalogLibrary.ResponseModels.Modification;
using CarCatalogLibrary.ResponseModels.Versions;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Subjects;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace CarCatalogLibrary
{
    public class Client
    {
        private GraphQLHttpClient graphQLCient;
        private GraphQLHttpClient CreateGraphQLClient(string url)
        {
            return new GraphQLHttpClient(url, new NewtonsoftJsonSerializer());
        }
        public Client(string url)
        {
            this.graphQLCient = CreateGraphQLClient(url);
        }

        public async Task<GraphQLResponse<T>> SendQueryAsync<T>(GraphQLHttpRequest request)
        {
            return await this.graphQLCient.SendQueryAsync<T>(request);
        }

        public async Task<GraphQLResponse<T>> SendQueryAsync<T>(string query)
        {
            var request = new GraphQLHttpRequest
            {
                Query = query
            };
            GraphQLResponse<T> result = null;
            var countIteration = 10;
            for (int i = 0; i <= countIteration; i++)
            {
                try
                {
                    result = await this.SendQueryAsync<T>(request);
                    break;
                }
                catch (Exception)
                {
                    if (i == countIteration)
                    {
                        throw;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Запрос для получения Версий
        /// </summary>
        /// <param name="first"></param>
        /// <param name="after"></param>
        /// <returns></returns>

        public string GenerateVersionsQuery(int first, string after = "") 
        {
            return  @"{
                        versions(first: [first], after: [after]) {
                          pageInfo {
                            hasNextPage
                            endCursor
                          }
                          edges {
                            node {
                              id
                              databaseId
                              name
                              model {
                                name
                              }
                            }
                          }
                        }
                      }".Replace("[first]", first.ToString()).Replace("[after]", "\"" + after + "\"");
        }
        /// <summary>
        /// запрос для получения модификаций
        /// </summary>
        /// <param name="first"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public string GenerateModificationsQuery(int first, string after = "")
        {
            return @"{
                        models(first: [first], after: [after]) {
                          pageInfo {
                            hasNextPage
                            endCursor
                          }
                          edges {
                            node {      
                                modification{          
                                  releaseDate
                                  startProductionYear
                                  model{
                                    name
                                    make{
                                      name
                                    }
                                  }
                                }        
                              }
                          }
                        }
                      }".Replace("[first]", first.ToString()).Replace("[after]", "\"" + after + "\"");
        }
        /// <summary>
        /// запрос для получения моделей
        /// </summary>
        /// <param name="first"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public string GenerateModelsQuery(int first, string after = "")
        {
            return @"{
                        models(first: [first], after: [after]) {
                          pageInfo {
                            hasNextPage
                            endCursor
                          }
                          edges {
                            node {
                              id
                              databaseId
                              name
                              make {
                                name
                              }
                            }
                          }
                        }
                      }".Replace("[first]", first.ToString()).Replace("[after]", "\"" + after + "\"");
        }
        /// <summary>
        /// запрос для получения марок
        /// </summary>
        /// <param name="first"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public string GenerateMakesQuery(int first, string after = "")
        {
            return @"{
                        makes(first: [first], after: [after]) {
                          pageInfo {
                            hasNextPage
                            endCursor
                          }
                          edges {
                            node {
                              id
                              databaseId
                              name
                            }
                          }
                        }
                      }".Replace("[first]", first.ToString()).Replace("[after]", "\"" + after + "\"");
        }
        /// <summary>
        /// Запрос на получение обрудования
        /// </summary>
        /// <param name="first"></param>
        /// <param name="after"></param>
        /// <param name="domain"></param>
        /// <param name="since"></param>
        /// <returns></returns>
        public string GenerateEquipmentsQuery(int first, string after = "", string domain = "STANDART", DateTime? since = null)
        {
            return @"{
                       equipments([first], [after], [domain], [since]) {
                          pageInfo {
                          hasNextPage
                          endCursor
                        }
                        edges {
                          node {
                            databaseId
                            modification {
                              releaseDate
                            }
                            modification {
                              releaseDate
                              startProductionYear
                              model {
                                name
                                make {
                                  name
                                }
                              }
                            }
                            version {
                              name
                            }
                            picture
                            colors {
                              name
                              metallic
                              hue
                              picture
                              pricelist{
                                price
                                productionFromDate
                                validFromDate
                              }
                            }
                          }
                          cursor
                        }
                       }
                     }".Replace("[first]", "first: " + first.ToString())
                       .Replace("[after]", "after: \"" + after + "\"")                       
                       .Replace("[domain]", "domain: " + domain)
                       .Replace("[since]", "since: \"" + (since.HasValue ? since.Value.ToString("yyyy-MM-dd") : "1900-01-01") + "\"");
        }
        /// <summary>
        /// Получить версии
        /// </summary>
        /// <param name="first"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public async Task<VersionsResponse> GetVersionsAsync(int first, string after = "") 
        {
            var query = this.GenerateVersionsQuery(first, after);

            var response = await this.SendQueryAsync<VersionsData>(query);

            return response.Data.versions;
        }
        /// <summary>
        /// получить модификации
        /// </summary>
        /// <param name="first"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public async Task<ModificationsResponse> GetModificationsAsync(int first, string after = "")
        {
            var query = this.GenerateModificationsQuery(first, after);

            var response = await this.SendQueryAsync<ModificationsData>(query);

            return response.Data.modifications;
        }
        /// <summary>
        /// получить модели
        /// </summary>
        /// <param name="first"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public async Task<ModelsResponse> GetModelsAsync(int first, string after = "")
        {
            var query = this.GenerateModelsQuery(first, after);

            var response = await this.SendQueryAsync<ModelsData>(query);

            return response.Data.models;
        }
        /// <summary>
        /// получить марки
        /// </summary>
        /// <param name="first"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public async Task<MakesResponse> GetMakesAsync(int first, string after = "")
        {
            var query = this.GenerateMakesQuery(first, after);

            var response = await this.SendQueryAsync<MakesData>(query);

            return response.Data.makes;
        }
        /// <summary>
        /// Получить обруддование
        /// </summary>
        /// <param name="first"></param>
        /// <param name="after"></param>
        /// <param name="domain"></param>
        /// <param name="since"></param>
        /// <returns></returns>
        public async Task<EquipmentsResponse> GetEquipmentsAsync(int first, string after = "", DateTime? since = null, string domain = "STANDART")
        {
            var query = this.GenerateEquipmentsQuery(first, after, domain, since);

            var response = await this.SendQueryAsync<EquipmentsData>(query);

            return response.Data.equipments;
        }
        /// <summary>
        /// получить оборудование
        /// </summary>
        /// <param name="first"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public async Task<EquipmentsResponse> GetEquipmentsAsync(int first, string after = "")
        {
            var query = this.GenerateEquipmentsQuery(first, after);

            var response = await this.SendQueryAsync<EquipmentsData>(query);

            return response.Data.equipments;
        }
        /// <summary>
        /// получить все версии
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<VersionsResponse> GetAllVersionsAsync() 
        {
            await foreach (var versions in this.GetDataAsync(this.GetVersionsAsync, 100, "")) 
            {
                yield return versions;
            }
        }
        /// <summary>
        /// получить все модификации
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<ModificationsResponse> GetAllModificationsAsync()
        {
            await foreach (var modifications in this.GetDataAsync(this.GetModificationsAsync, 100, ""))
            {
                yield return modifications;
            }
        }
        /// <summary>
        /// получить все модели
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<ModelsResponse> GetAllModelsAsync()
        {
            await foreach (var models in this.GetDataAsync(this.GetModelsAsync, 100, ""))
            {
                yield return models;
            }
        }
        /// <summary>
        /// получить все марки
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<MakesResponse> GetAllMakesAsync()
        {
            await foreach (var makes in this.GetDataAsync(this.GetMakesAsync, 100, ""))
            {
                yield return makes;
            }
        }
        /// <summary>
        /// получить все оборудования
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<EquipmentsResponse> GetAllEquipmentsAsync(int first, string after = "", DateTime? since = null, string domain = "STANDART")
        {
            await foreach (var equipments in this.GetDataAsync(this.GetEquipmentsAsync, first, after, since))
            {               
                yield return equipments;
            }
        }
        /// <summary>
        /// Обобщающий метод для запроса
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="action"></param>
        /// <param name="first"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<TResult> GetDataAsync<TResult>(Func<int, string, Task<TResult>> action, int first, string after = "", DateTime? since = null) where TResult : ResponseModels.BaseInfo
        {
            var hasNextPage = true;
            while (hasNextPage)
            {
                var result = await action(first, after);

                hasNextPage = result.pageInfo.hasNextPage;
                after = result.pageInfo.endCursor;
                yield return result;
            }
        }
    }
}
