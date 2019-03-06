using ESRI.ArcGIS.Geodatabase;
using System;
using ESRI.ArcGIS.Geometry;
namespace JT.ArcObject.Extension {

    public static partial class JTFeatureClassExt {



    }

    public static partial class JTFeatureClassExt {

    }

    public static partial class JTFeatureClassExt {

        public static bool JTExistField(this IFeatureClass fc, string fieldname) {
            if (fc.FindField(fieldname) >= 0) { return true; }
            return false;
        }

    }

    public static partial class JTFeatureClassExt {

        public static void JTClearFeatures(this IFeatureClass fc) {
            var cursor = fc.Update(null, true);
            IFeature feature = null;
            while ((feature = cursor.NextFeature()) != null) {
                cursor.DeleteFeature();
            }
        }

        public static void JTDeleteFeatures(this IFeatureClass fc, IQueryFilter queryFilter) {
            var table = fc as ITable;
            table.JTClear(queryFilter);
        }

    }

    public static partial class JTFeatureClassExt {

        public static bool JTContain(this IFeatureClass fc, IFeature f) {
            var shape = f.ShapeCopy;
            if (shape == null || shape.IsEmpty) { return false; }

            var cursor = fc.Search(new SpatialFilterClass { Geometry = shape, SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects }, true);

            IFeature fea = null;
            while ((fea = cursor.NextFeature()) != null) {
                var s = fea.Shape;
                if (s == null || s.IsEmpty) { continue; }
                var ro = s as IRelationalOperator;
                if (ro.Contains(shape)) {
                    ADF.ComReleaser.ReleaseComObject(s);
                    ADF.ComReleaser.ReleaseComObject(fea);
                    ADF.ComReleaser.ReleaseComObject(cursor);
                    ADF.ComReleaser.ReleaseComObject(shape);
                    return true;
                } else {
                    var to = shape as ITopologicalOperator;
                    shape = to.Difference(s);
                    if (shape == null || shape.IsEmpty) {
                        ADF.ComReleaser.ReleaseComObject(s);
                        ADF.ComReleaser.ReleaseComObject(fea);
                        ADF.ComReleaser.ReleaseComObject(cursor);
                        ADF.ComReleaser.ReleaseComObject(shape);
                        return true;
                    }
                }
                s = null;
            }

            ADF.ComReleaser.ReleaseComObject(cursor);
            ADF.ComReleaser.ReleaseComObject(shape);

            GC.Collect();
            return false;
        }

    }

    public static partial class JTFeatureClassExt {
        public static string JTFeatureClassName(this IFeatureClass fc) {
            return (fc as IDataset).Name;
        }
    }

    public static partial class JTFeatureClassExt {
        public static void JTUpdateExtent(this IFeatureClass fc) {
            if (fc == null) { return; }
            if (fc is IFeatureClassManage fcm) { fcm.UpdateExtent(); }
        }
    }

    public static partial class JTFeatureClassExt {

    }

    public static partial class JTFeatureClassExt {

    }

    public static partial class JTFeatureClassExt {

        public static void JTDelete(this IFeatureClass fc) {
            var ds = fc as IDataset;
            ds.JTDelete();
        }

    }




}
