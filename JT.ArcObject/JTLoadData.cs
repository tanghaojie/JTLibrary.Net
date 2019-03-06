using ESRI.ArcGIS.EditorExt;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JT.ADF;
namespace JT.ArcObject {
    public static partial class JTLoadData {
        public class JTLoadDataError {
            internal JTLoadDataError(int oid, string description) {
                ObjectID = oid;
                ErrorDescription = description;
            }
            public int ObjectID { get; }
            public string ErrorDescription { get; }
        }

        public static bool LoadData_Loader(ITable sourceTable, ITable targetTable, out JTLoadDataError[] errors) {
            if (sourceTable == null || targetTable == null) { throw new ArgumentNullException(); }

            IFields sFields = sourceTable.Fields;
            IFields tFields = targetTable.Fields;

            string sourceFieldsStr = string.Empty;

            var tFieldCount = tFields.FieldCount;
            if (tFieldCount > 0) {
                for (int i = 0; i < tFieldCount; i++) {
                    var tField = tFields.Field[i];
                    if (!tField.Editable) { continue; }
                    var tFieldname = tField.Name;
                    var sFieldIndex = sFields.FindField(tFieldname);
                    if (sFieldIndex >= 0) {
                        sourceFieldsStr += tFieldname + ",";
                    } else {
                        sourceFieldsStr += "'',";
                    }
                }
            }

            IObjectLoader objectLoader = new ObjectLoaderClass();
            IQueryFilter qf = new QueryFilterClass { SubFields = sourceFieldsStr, };
            objectLoader.LoadObjects(null, sourceTable, qf, targetTable, tFields, false, 0, false, false, 20, out IEnumInvalidObject enumInvalidObject);

            var listErrors = new List<JTLoadDataError>();
            IInvalidObjectInfo invalidObject = null;
            while ((invalidObject = enumInvalidObject.Next()) != null) { listErrors.Add(new JTLoadDataError(invalidObject.InvalidObjectID, invalidObject.ErrorDescription)); }

            ComReleaser.ReleaseComObject(tFields);
            ComReleaser.ReleaseComObject(sFields);
            ComReleaser.ReleaseComObject(objectLoader);
            ComReleaser.ReleaseComObject(qf);

            errors = listErrors.ToArray();
            return errors.Length <= 0;
        }
    }

    public static partial class JTLoadData {
        /// <summary>
        /// not finish
        /// </summary>
        /// <param name="sourceFeatureClass"></param>
        /// <param name="targetFeatureClass"></param>
        /// <param name="queryClause"></param>
        public static void LoadData_Insert(IFeatureClass sourceFeatureClass, IFeatureClass targetFeatureClass, string queryClause) {
            using (ComReleaser comReleaser = new ComReleaser()) {
                IFeatureBuffer featureBuffer = targetFeatureClass.CreateFeatureBuffer();
                comReleaser.ManageLifetime(featureBuffer);

                IFeatureCursor insertCursor = targetFeatureClass.Insert(true);
                comReleaser.ManageLifetime(insertCursor);

                IQueryFilter queryFilter = null;
                if (!string.IsNullOrEmpty(queryClause)) { queryFilter = new QueryFilterClass { WhereClause = queryClause }; }
                IFeatureCursor cursor = sourceFeatureClass.Search(queryFilter, true);

                var sourceFields = sourceFeatureClass.Fields;
                var sourceFieldCount = sourceFields.FieldCount;

                IFeature sourceFeature = null; ;
                while ((sourceFeature = cursor.NextFeature()) != null) {
                    var shape = sourceFeature.ShapeCopy;
                    featureBuffer.Shape = shape;

                    for (int i = 0; i < sourceFeature.Fields.FieldCount; i++) {
                        IField field = sourceFeature.Fields.get_Field(i);
                        if (field.Type != esriFieldType.esriFieldTypeOID && field.Type != esriFieldType.esriFieldTypeGeometry && field.Type != esriFieldType.esriFieldTypeGlobalID && field.Type != esriFieldType.esriFieldTypeGUID) {
                            string fieldName = field.Name;
                            int index = featureBuffer.Fields.FindField(fieldName);
                            if (index > -1 && fieldName != "Shape_Length" && fieldName != "Shape_Area")
                                featureBuffer.set_Value(index, sourceFeature.get_Value(i));
                        }

                    }
                    insertCursor.InsertFeature(featureBuffer);
                    ComReleaser.ReleaseComObject(shape);
                }

                // Flush the buffer to the geodatabase.
                insertCursor.Flush();
                ComReleaser.ReleaseComObject(cursor);

            }
        }
    }

    public static partial class JTLoadData {
        /// <summary>
        /// not finish
        /// </summary>
        /// <param name="sourceFeatureClass"></param>
        /// <param name="targetFeatureClass"></param>
        //public static void LoadData_LoadOnlyMode(IFeatureClass sourceFeatureClass, IFeatureClass targetFeatureClass) {
        //    // Cast the feature class to the IFeatureClassLoad interface.
        //    IFeatureClassLoad featureClassLoad = (IFeatureClassLoad)targetFeatureClass;

        //    // Acquire an exclusive schema lock for the class.
        //    ISchemaLock schemaLock = (ISchemaLock)targetFeatureClass;
        //    try {
        //        schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);

        //        // Enable load-only mode on the feature class.
        //        featureClassLoad.LoadOnlyMode = true;
        //        using (ComReleaser comReleaser = new ComReleaser()) {
        //            // Create the feature buffer.
        //            IFeatureBuffer featureBuffer = targetFeatureClass.CreateFeatureBuffer();
        //            comReleaser.ManageLifetime(featureBuffer);

        //            // Create an insert cursor.
        //            IFeatureCursor insertCursor = targetFeatureClass.Insert(true);
        //            comReleaser.ManageLifetime(insertCursor);

        //            IQueryFilter queryFilter = new QueryFilterClass();
        //            queryFilter.WhereClause = queryClause;
        //            IFeatureCursor cursor = sourceFeatureClass.Search(queryFilter, true);

        //            IFeature sourceFeature = cursor.NextFeature();

        //            while (sourceFeature != null) {
        //                //如果是线或面要素类需要执行下Simplify，这里用的点要素类，不做验证了
        //                featureBuffer.Shape = sourceFeature.ShapeCopy;

        //                for (int i = 0; i < sourceFeature.Fields.FieldCount; i++) {
        //                    IField field = sourceFeature.Fields.get_Field(i);
        //                    if (field.Type != esriFieldType.esriFieldTypeOID && field.Type != esriFieldType.esriFieldTypeGeometry && field.Type != esriFieldType.esriFieldTypeGlobalID && field.Type != esriFieldType.esriFieldTypeGUID) {
        //                        string fieldName = field.Name;
        //                        int index = featureBuffer.Fields.FindField(fieldName);
        //                        if (index > -1 && fieldName != "Shape_Length" && fieldName != "Shape_Area")
        //                            featureBuffer.set_Value(index, sourceFeature.get_Value(i));
        //                    }

        //                }
        //                insertCursor.InsertFeature(featureBuffer);
        //                sourceFeature = cursor.NextFeature();
        //            }

        //            // Flush the buffer to the geodatabase.
        //            insertCursor.Flush();
        //            ComReleaser.ReleaseCOMObject(cursor);
        //            IFeatureClassManage targetFeatureClassManage = targetFeatureClass as IFeatureClassManage;
        //            targetFeatureClassManage.UpdateExtent();
        //        }
        //    } catch (Exception) {
        //        // Handle the failure in a way appropriate to the application.
        //        MessageBox.Show("无法获取该要素类的排它锁，检查ArcMap是否打开了该要素类，建议关闭！");
        //    } finally {
        //        // Disable load-only mode on the feature class.
        //        featureClassLoad.LoadOnlyMode = false;

        //        // Demote the exclusive schema lock to a shared lock.
        //        schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
        //    }

        //}
    }
    public static partial class JTLoadData {
        /// <summary>
        /// not finish
        /// </summary>
        /// <param name="sourceFeatureClass"></param>
        /// <param name="targetFeatureClass"></param>
        //public void LoadData_Write(IFeatureClass sourceFeatureClass, IFeatureClass targetFeatureClass) {
        //    IQueryFilter queryFilter = new QueryFilterClass();
        //    queryFilter.WhereClause = queryClause;
        //    IFeatureCursor cursor = sourceFeatureClass.Search(queryFilter, true);
        //    IFeature sourceFeature = cursor.NextFeature();
        //    IFeatureClassWrite featureClassWrite = targetFeatureClass as IFeatureClassWrite;
        //    ISet setAdd = new SetClass();
        //    while (sourceFeature != null) {
        //        IFeature targetFeature = targetFeatureClass.CreateFeature();

        //        //如果是线或面要素类需要执行下Simplify，这里用的点要素类，不做验证了
        //        targetFeature.Shape = sourceFeature.ShapeCopy;

        //        for (int i = 0; i < sourceFeature.Fields.FieldCount; i++) {
        //            IField field = sourceFeature.Fields.get_Field(i);
        //            if (field.Type != esriFieldType.esriFieldTypeOID && field.Type != esriFieldType.esriFieldTypeGeometry && field.Type != esriFieldType.esriFieldTypeGlobalID && field.Type != esriFieldType.esriFieldTypeGUID) {
        //                string fieldName = field.Name;
        //                int index = targetFeature.Fields.FindField(fieldName);
        //                if (index > -1 && fieldName != "Shape_Length" && fieldName != "Shape_Area")
        //                    targetFeature.set_Value(index, sourceFeature.get_Value(i));
        //            }

        //        }
        //        //setAdd.Add(targetFeature);
        //        featureClassWrite.WriteFeature(targetFeature);
        //        sourceFeature = cursor.NextFeature();

        //    }
        //    //featureClassWrite.WriteFeatures(setAdd);//与WriteFeature没啥效率上的区别
        //    ComReleaser.ReleaseCOMObject(cursor);

        //    IFeatureClassManage targetFeatureClassManage = targetFeatureClass as IFeatureClassManage;
        //    targetFeatureClassManage.UpdateExtent();
        //}
    }
}
