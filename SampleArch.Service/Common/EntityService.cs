using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleArch.Model;
using SampleArch.Repository;
using SampleArch.Model.Core;
using SampleArch.Repository.Admin;
using SampleArch.Model.Models;
using System.Reflection;
using System.Linq.Expressions;
using SampleArch.Service.Core;

namespace SampleArch.Service
{
    public abstract class EntityService<T> : IEntityService<T>  
        where T :  BaseEntity
    {
        public IUnitOfWork UnitOfWork;
        public IGenericRepository<T> TheRepository;
        public IMultilanguageRepository MLRepository;
        public ISessionManager SessionManager;
        public IUILanguageRepository LangRepository;

        public EntityService(IUnitOfWork unitOfWork, 
            IGenericRepository<T> repository,   
            IMultilanguageRepository mlRepository, 
            IUILanguageRepository langRepository,  
            ISessionManager sessionManager)
        {
            UnitOfWork = unitOfWork;
            TheRepository = repository;
            MLRepository = mlRepository;
            SessionManager = sessionManager;
            LangRepository = langRepository;
        }     



        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            TheRepository.Add(entity);
            UnitOfWork.Commit(); 

            if (HasEntityMultilanguageField())
            {
                CreateMultilanguageDefinitions(entity);
            }

            UnitOfWork.Commit();         
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            TheRepository.Edit(entity);

            if (HasEntityMultilanguageField())
            {
                UpdateMultilanguageDefinitions(entity);
            }

            UnitOfWork.Commit();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            if (HasEntityMultilanguageField())
            {
                DeleteMultilanguageDefinitions(entity);
            }
            TheRepository.Delete(entity);
            UnitOfWork.Commit();
        }




        public virtual IEnumerable<T> GetAll()
        {
            List<T> result = TheRepository.GetAll().ToList();

            if (HasEntityMultilanguageField())
            {
                LocalizeList(result);
            }
            return result;
        }

        public virtual IEnumerable<T> GetByFilter(Expression<Func<T, bool>> predicate)
        {
            List<T> result = null;

            if (predicate == null)
            {
                result = TheRepository.GetAll().ToList();
            }
            else
            {
                result = TheRepository.FindBy(predicate).ToList();
            }

            if (HasEntityMultilanguageField())
            {
                LocalizeList(result);
            }

            return result;
        }

        public IEnumerable<T> GetByFilterWithInclude(string includings, Expression<Func<T, bool>> predicate)
        {
            List<T> result = null;

            if (predicate == null)
            {
                result = TheRepository.GetAllWithInclude(includings).ToList();
            }
            else
            {
                result = TheRepository.FindByWithInclude(includings, predicate).ToList();
            }

            if (HasEntityMultilanguageField())
            {
                LocalizeList(result);
            }

            return result;
        }

        public IEnumerable<T> GetAllWithIncludings(string includings)
        {
            List<T> result = TheRepository.GetAllWithInclude(includings).ToList();

            if (HasEntityMultilanguageField())
            {
                LocalizeList(result);
            }

            if (HasEntityMultilanguageObject())
            {
                foreach (T item in result)
                {
                    LocalizeIncludingItem(item);
                }
            }

            return result;
        }

        public virtual T Get(object val)
        {
            T model =  TheRepository.Get(val);

            if (HasEntityMultilanguageField())
            {
                LocalizeItem(model);
            }

            return model;
        }

        public T GetWithIncludings(object val, string includings)
        {
            T model =TheRepository.GetWithInclude(val, includings);

            if (HasEntityMultilanguageField())
            {
                LocalizeItem(model);
            }

            if (HasEntityMultilanguageObject())
            {
                LocalizeIncludingItem(model);
            }

            return model;
        }

        protected Boolean HasEntityMultilanguageField()
        {
            List<PropertyInfo> props = (from p in typeof(T).GetProperties()
                                        let attr = p.GetCustomAttributes(typeof(MultilanguageFieldAttribute), true)
                                        where attr.Length == 1
                                        select p).ToList();

            if (props != null && props.Count > 0)
            {
                return true;
            }
            return false;
        }

        protected Boolean HasEntityMultilanguageObject()
        {
            List<PropertyInfo> props = (from p in typeof(T).GetProperties()
                                        let attr = p.GetCustomAttributes(typeof(MultilanguageEntityAttribute), true)
                                        where attr.Length == 1
                                        select p).ToList();

            if (props != null && props.Count > 0)
            {
                return true;
            }
            return false;
        }

        protected void CreateMultilanguageDefinitions(T model)
        {
            var langs = LangRepository.GetAll().ToList();

            string entityType = typeof(T).ToString();
            string currentlanguage = SessionManager.GetUserProfileLang().ToUpper();

            foreach (UILanguage lg in langs)
            {
                var props = from p in model.GetType().GetProperties()
                            let attr = p.GetCustomAttributes(typeof(MultilanguageFieldAttribute), true)
                            where attr.Length == 1
                            select new { Property = p, Attribute = attr.First() as MultilanguageFieldAttribute };


                foreach (var mlField in props)
                {
                    PropertyInfo pCode = model.GetType().GetProperty(mlField.Attribute.TheCodeField);

                    MultiLanguage langCurrent = new MultiLanguage();
                    langCurrent.EntityType = typeof(T).ToString();
                    langCurrent.Code = pCode.GetValue(model).ToString();
                    langCurrent.LanguageCode = lg.LangCode.ToUpper();

                    MultiLanguage mLang = MLRepository.GetBy(p => p.Code == langCurrent.Code && p.EntityType == entityType && p.LanguageCode == langCurrent.LanguageCode);

                    if (mLang == null || mLang.Id == 0)
                    {
                        mLang = new MultiLanguage();
                        mLang.EntityType = entityType;
                        mLang.Code = pCode.GetValue(model).ToString();
                        mLang.LanguageCode = lg.LangCode.ToUpper();
                        mLang.Value = mlField.Property.GetValue(model).ToString();

                        MLRepository.Add(mLang);
                    }
                    else
                    {
                        mLang.Value = mlField.Property.GetValue(model).ToString();
                        MLRepository.Edit(mLang);
                    }
                   
                }
            }
        }

        protected void UpdateMultilanguageDefinitions(T model)
        {
            string currentlanguage = SessionManager.GetUserProfileLang().ToUpper();

            var props = from p in model.GetType().GetProperties()
                        let attr = p.GetCustomAttributes(typeof(MultilanguageFieldAttribute), true)
                        where attr.Length == 1
                        select new { Property = p, Attribute = attr.First() as MultilanguageFieldAttribute };

            foreach (var mlField in props)
            {
                PropertyInfo pCode = model.GetType().GetProperty(mlField.Attribute.TheCodeField);

                MultiLanguage langCurrent = new MultiLanguage();
                langCurrent.EntityType = typeof(T).ToString();
                langCurrent.Code = pCode.GetValue(model).ToString();
                langCurrent.LanguageCode = currentlanguage;


                string entityType = typeof(T).ToString();

                MultiLanguage mLang = MLRepository.GetBy(p => p.Code == langCurrent.Code && p.EntityType == entityType && p.LanguageCode == currentlanguage);

                if (mLang == null || mLang.Id == 0)
                {
                    mLang = new MultiLanguage();
                    mLang.EntityType = entityType;
                    mLang.Code = pCode.GetValue(model).ToString();
                    mLang.LanguageCode = currentlanguage;
                    mLang.Value = mlField.Property.GetValue(model).ToString();

                    MLRepository.Add(mLang);
                }
                else
                {
                    mLang.Value = mlField.Property.GetValue(model).ToString();
                    MLRepository.Edit(mLang);
                }
            }
        }

        protected void DeleteMultilanguageDefinitions(T model)
        {
            var langs = LangRepository.GetAll().ToList();

            string entityType = typeof(T).ToString();

            foreach (UILanguage lg in langs)
            {
                var props = from p in model.GetType().GetProperties()
                            let attr = p.GetCustomAttributes(typeof(MultilanguageFieldAttribute), true)
                            where attr.Length == 1
                            select new { Property = p, Attribute = attr.First() as MultilanguageFieldAttribute };


                foreach (var mlField in props)
                {
                    PropertyInfo pCode = model.GetType().GetProperty(mlField.Attribute.TheCodeField);

                    MultiLanguage langCurrent = new MultiLanguage();
                    langCurrent.EntityType = typeof(T).ToString();
                    langCurrent.Code = pCode.GetValue(model).ToString();
                    langCurrent.LanguageCode = lg.LangCode.ToUpper();

                    MultiLanguage mLang = MLRepository.GetBy(p => p.Code == langCurrent.Code && p.EntityType == entityType && p.LanguageCode == langCurrent.LanguageCode);

                    if (mLang != null && mLang.Id > 0)
                    {
                        MLRepository.Delete(mLang);
                    }
                }
            }
        }

        public void LocalizeList(List<T> models)
        {
            var props = from p in (typeof(T)).GetProperties()
                        let attr = p.GetCustomAttributes(typeof(MultilanguageFieldAttribute), true)
                        where attr.Length == 1
                        select new { Property = p, Attribute = attr.First() as MultilanguageFieldAttribute };


            string entityType = typeof(T).ToString();
            string currentlanguage = SessionManager.GetUserProfileLang().ToUpper();

            List<MultiLanguage> theDictionary = MLRepository.GetAll().Where(l => l.EntityType == entityType && l.LanguageCode == currentlanguage).ToList();

            foreach (var mlField in props)
            {
                PropertyInfo pCode = typeof(T).GetProperty(mlField.Attribute.TheCodeField);

                foreach (object model in models)
                {
                    string code = pCode.GetValue(model).ToString();

                    MultiLanguage mLang = theDictionary.FirstOrDefault(l => l.Code == code);

                    if (mLang != null)
                    {
                        mlField.Property.SetValue(model, mLang.Value);
                    }
                }
            }
        }

        public void LocalizeItem(T model)
        {
            var props = from p in (typeof(T)).GetProperties()
                        let attr = p.GetCustomAttributes(typeof(MultilanguageFieldAttribute), true)
                        where attr.Length == 1
                        select new { Property = p, Attribute = attr.First() as MultilanguageFieldAttribute };


            string entityType = typeof(T).ToString();
            string currentlanguage = SessionManager.GetUserProfileLang().ToUpper();

            List<MultiLanguage> theDictionary = MLRepository.GetAll().Where(l => l.EntityType == entityType && l.LanguageCode == currentlanguage).ToList();

            foreach (var mlField in props)
            {
                PropertyInfo pCode = typeof(T).GetProperty(mlField.Attribute.TheCodeField);

                string code = pCode.GetValue(model).ToString();

                MultiLanguage mLang = theDictionary.FirstOrDefault(l => l.Code == code);

                if (mLang != null)
                {
                    mlField.Property.SetValue(model, mLang.Value);
                }
            
            }
        }

        public void LocalizeIncludingItem(T model)
        {
            var pEntity = from p in typeof(T).GetProperties()
                        let attr = p.GetCustomAttributes(typeof(MultilanguageEntityAttribute), true)
                        where attr.Length == 1
                        select new { Property = p, Attribute = attr.First() as MultilanguageEntityAttribute };

            string currentlanguage = SessionManager.GetUserProfileLang().ToUpper();

            foreach (var mlField in pEntity)
            {
                string entityType = mlField.Property.PropertyType.ToString();

                object pObject = mlField.Property.GetValue(model);

                var pItem = from p in pObject.GetType().GetProperties()
                            let attr = p.GetCustomAttributes(typeof(MultilanguageFieldAttribute), true)
                            where attr.Length == 1
                            select new { Property = p, Attribute = attr.First() as MultilanguageFieldAttribute };

                List<MultiLanguage> theDictionary = MLRepository.GetAll().Where(l => l.EntityType == entityType && l.LanguageCode == currentlanguage).ToList();

                foreach (var subFieldProp in pItem)
                {
                    PropertyInfo pCode = pObject.GetType().GetProperty(subFieldProp.Attribute.TheCodeField);

                    string code = pCode.GetValue(pObject).ToString();

                    MultiLanguage mLang = theDictionary.FirstOrDefault(l => l.Code == code);

                    if (mLang != null)
                    {
                        subFieldProp.Property.SetValue(pObject, mLang.Value);
                    }
                }
            }
        }





        public IEnumerable<ValidationResult> GetBussinesValidations(BaseEntity model)
        {
            return new List<ValidationResult>();
        }


    }
}
