﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using System.Collections;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.Services
{
   public class AssetService : Contracts.IAssetService
    {


        public IEnumerable<Entity.Model.Asset> GetList(bool Enabled)
        {
            IAssetComponent component = new AssetComponent();
            IEnumerable<Asset> result = null;

            if (Enabled)
            {
                result = component.GetEnabledList();
            }
            else
            {
                result = component.GetList();
            }
            return result;

         //   throw new NotImplementedException();
        }

        public bool Save(Entity.Model.Asset asset)
        {
            IAssetComponent component = new AssetComponent();
            try
            {
                component.Save(asset);
            }
            catch (Exception ex)
            {
                
                throw new FaultException(ex.Message);
            }
            return true;
           // throw new NotImplementedException();
        }

        public bool Delete(Entity.Model.Asset entity)
        {
            IAssetComponent component = new AssetComponent();
            try
            {
                component.Delete(entity);
            }
            catch (Exception ex)
            {
                
                throw new FaultException(ex.Message);
            }
            return true;
           
           // throw new NotImplementedException();
        }
    }
}
