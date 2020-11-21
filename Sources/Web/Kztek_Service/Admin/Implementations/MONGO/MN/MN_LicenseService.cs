using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kztek_Core.Models;
using Kztek_Data.Repository.Mongo;
using Kztek_Library.Helpers;
using Kztek_Model.Models.MN;
using Kztek_Service.Admin.Interfaces.MN;

namespace Kztek_Service.Admin.Implementations.MONGO.MN
{
    public class MN_LicenseService : IMN_LicenseService
    {
        private IMN_LicenseRepository _MN_LicenseRepository;

        public MN_LicenseService(IMN_LicenseRepository _MN_LicenseRepository)
        {
            this._MN_LicenseRepository = _MN_LicenseRepository;
        }

        public async Task<MessageReport> Create(MN_License model)
        {
            return await _MN_LicenseRepository.Add(model);
        }

        public async Task<MessageReport> Delete(string id)
        {
            var result = new MessageReport(false, "Có lỗi xảy ra");

            var obj = await GetById(id);
            if (obj != null)
            {
                var query = new StringBuilder();
                query.AppendLine("{");
                query.AppendLine("'_id': { '$eq': '" + id + "' }");
                query.AppendLine("}");

                return await _MN_LicenseRepository.Remove(MongoHelper.ConvertQueryStringToDocument(query.ToString()));
            }
            else
            {
                result = new MessageReport(false, "Bản ghi không tồn tại");
            }

            return await Task.FromResult(result);
        }

        public async Task<MN_License> GetById(string id)
        {
            return await _MN_LicenseRepository.GetOneById(id);
        }

        public async Task<MN_License> GetByName(string name)
        {
            var query = new StringBuilder();
            query.AppendLine("{");
            query.AppendLine("'ProjectName': { '$eq': '" + name + "' }");
            query.AppendLine("}");

            var data = await _MN_LicenseRepository.GetManyToList(MongoHelper.ConvertQueryStringToDocument(query.ToString()));

            return data.FirstOrDefault();
        }

        public async Task<GridModel<MN_License>> GetPaging(string key,string fromdate, string todate, int pageNumber, int pageSize)
        {

         

            var query = new StringBuilder();
            
            query.AppendLine("{");

            if (!string.IsNullOrWhiteSpace(key))
            {
                query.AppendLine("'$or': [");

                query.AppendLine("{ 'Name': { '$in': [/" + key + "/i] } }");
                query.AppendLine(", { 'ProjectName': { '$in': [/" + key + "/i] } }");

                query.AppendLine("]");
            }

            query.AppendLine("}");

            var sort = new StringBuilder();
            sort.AppendLine("{");
            sort.AppendLine("'ProjectName': 1");
            sort.AppendLine("}");
           
            return await _MN_LicenseRepository.GetPaging(MongoHelper.ConvertQueryStringToDocument(query.ToString()), MongoHelper.ConvertQueryStringToDocument(sort.ToString()), pageNumber, pageSize);
        }

        public async Task<GridModel<MN_LicenseCustom>> GetPagings(string key, string fromdate, string todate, int page, int pagesize)
        {
            if (!string.IsNullOrWhiteSpace(fromdate))
            {
                fromdate = Convert.ToDateTime(fromdate).ToString("dd/MM/yyyy HH:mm");
            }
            if (!string.IsNullOrWhiteSpace(todate))
            { 
                todate = Convert.ToDateTime(todate).ToString("dd/MM/yyyy HH:mm");
            }

            var license = from n in _MN_LicenseRepository.Table
                          select n;
            //var ex = license.Select(n => n.ExpireDate).ToList();

            var lists = new List<MN_LicenseCustom>();
            foreach (var item in license)
            {
                var obj = new MN_LicenseCustom() {
                    Id = item.Id,
                    IsExpire = item.IsExpire,
                    ProjectName = item.ProjectName,
                    ExpireDate = Convert.ToDateTime(item.ExpireDate)
            
            };

                lists.Add(obj);
            }
            lists.Where(n => n.ExpireDate > Convert.ToDateTime(fromdate)  && n.ExpireDate < Convert.ToDateTime(todate));
           
                        


            //var exprie = license.FirstOrDefault(n => n.)
            //for (var i = 0; i < ex.ToString().Length; i++)
            //{
            //    obj.ExpireDate = Convert.ToDateTime(ex[i]).ToString("yyyy-MM-dd");
            //}

                             //for (int i = 0; i < ex.Count; i++)
                             //{
                             //    dates = Convert.ToDateTime(ex[i]);
                             //}



            var str = new StringBuilder();
            str.AppendLine("{");
            if (!string.IsNullOrWhiteSpace(key))
            {

                str.AppendLine("'ProjectName' :{'$in' : [/" + key + "/i]}");
            }
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate))
            {

            }
            //str.AppendLine("'$and' : [");
            //str.AppendLine("{'ExpireDate' : {'$gt' : new Date('" + fromdate + "')}} ,");
            //str.AppendLine("{'ExpireDate' : {'$lt' : new Date('" + todate + "')}}");
            //str.AppendLine("]");
            str.AppendLine("}");
            return await _MN_LicenseRepository.GetPagings(MongoHelper.ConvertQueryStringToDocument(str.ToString()), page, pagesize);
        }

        public async Task<MessageReport> Update(MN_License model)
        {
            var query = new StringBuilder();
            query.AppendLine("{");
            query.AppendLine("'_id': { '$eq': '" + model.Id + "' }");
            query.AppendLine("}");

            return await _MN_LicenseRepository.Update(MongoHelper.ConvertQueryStringToDocument(query.ToString()), model);
        }
    }
}
