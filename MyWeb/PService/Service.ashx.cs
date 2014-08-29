﻿using System;
using System.Collections.Generic;
using System.Web;
using MyLoad.LoadStatic;
using MyLoad.LoadNews;
using MyUtility;
using System.Text;
using MyBase.MyWeb;
using MyLoad.LoadService;

namespace MyWeb.PService
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    public class Service : MyASHXBase
    {
        private int ServiceGroupID = 0;

        public override void WriteHTML()
        {
            try
            {

                LoadHeader mHeader = new LoadHeader();
                Write(mHeader.GetHTML());

                LoadBanner mBanner = new LoadBanner();
                Write(mBanner.GetHTML());

                if (Request.QueryString["gid"] != null)
                {
                    int.TryParse(Request.QueryString["gid"], out ServiceGroupID);
                }
                LoadContent mContent = new LoadContent();
                mContent.InsertHTML_Change += new LoadContent.InsertHTML(mContent_InsertHTML_Change);
                Write(mContent.GetHTML());
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, false, MyNotice.EndUserError.LoadDataError, "Chilinh");
                Write(MyNotice.EndUserError.LoadDataError);
            }
            finally
            {
                LoadFooter mFooter = new LoadFooter();
                Write(mFooter.GetHTML());
            }
        }

        string mContent_InsertHTML_Change()
        {
            try
            {
                StringBuilder mBuilder = new StringBuilder(string.Empty);
                LoadListGroup mGroup = new LoadListGroup();
                mBuilder.Append(mGroup.GetHTML());

                LoadListService mService = new LoadListService(ServiceGroupID);
                mBuilder.Append(mService.GetHTML());

                return mBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}