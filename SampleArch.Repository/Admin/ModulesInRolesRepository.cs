using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Admin
{
  
    public class ModulesInRoleRepository : GenericRepository<ModulesInRole>, IModulesInRoleRepository
    {
        ISessionManager _sessionManeger;
        public ModulesInRoleRepository(DbContext context, ISessionManager sessionManeger)
            : base(context)
        {
            _sessionManeger = sessionManeger;
        }

        public List<ModulesInRolesViewModel> GetCartesian()
        {
            string roleEntityType = typeof(Role).ToString();
            string modelEntityType = typeof(Role).ToString();

            string languageCode = _sessionManeger.GetUserProfileLang();

            SampleArchContext ctx = _entities as SampleArchContext;

            IQueryable<ModulesInRolesViewModel> cartesianBase  = ( from m in ctx.Modules
                                                  from r in ctx.Roles 
                                                  join ml in ctx.MultiLanguage on new { code = m.Code, Lang = languageCode, ent = modelEntityType } equals new { code = ml.Code, Lang = ml.LanguageCode, ent = ml.EntityType } into ml1
                                                  from ml in ml1.DefaultIfEmpty(null)
                                                  join rl in ctx.MultiLanguage on new { code = r.Code, Lang = languageCode, ent = roleEntityType } equals new { code = rl.Code, Lang = rl.LanguageCode, ent = rl.EntityType } into rl1
                                                  from rl in rl1.DefaultIfEmpty(null)
                                                  join mir in ctx.ModulesInRoles on new { roleId = r.Id, moduleId = m.Id } equals new { roleId = mir.RoleId, moduleId = mir.ModuleId } into mir1
                                                  from mir in mir1.DefaultIfEmpty(null)
                                                  select new    ModulesInRolesViewModel()
                                                  {
                                                      Id = mir == null ? 0 : mir.Id,
                                                      RoleId = r.Id,
                                                      RoleName = rl == null ? r.Code : rl.Value,
                                                      ModuleId = m.Id,
                                                      ModuleName = ml == null ? m.Code : ml.Value,
                                                      Add  =  mir == null ? false : mir.Add,  
                                                      Delete  =  mir == null ? false : mir.Delete, 
                                                      Update  =  mir == null ? false : mir.Update, 
                                                      View  =  mir == null ? false : mir.View, 
                                                      CreateUserId = mir == null ? 0 : mir.CreateUserId,
                                                      CreateDate = mir == null ? DateTime.MinValue : mir.CreateUserDate,
                                                      UpdateUserId = mir == null ? null : mir.UpdateUserId,
                                                      UpdateDate = mir == null ? null: mir.UpdateUserDate
                                                  });

            var result = cartesianBase.ToList();

            return result;
                                                   
        }
    }
}
