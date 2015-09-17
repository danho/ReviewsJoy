//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Entity.Infrastructure.MappingViews;

[assembly: DbMappingViewCacheTypeAttribute(
    typeof(ReviewsJoy.DAL.DatabaseContext),
    typeof(Edm_EntityMappingGeneratedViews.ViewsForBaseEntitySetsbbd663a39f2c4e8f0688378276dab5f1f199ab4f3ad7575b9497b86695ac1b48))]

namespace Edm_EntityMappingGeneratedViews
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Core.Metadata.Edm;

    /// <summary>
    /// Implements a mapping view cache.
    /// </summary>
    [GeneratedCode("Entity Framework Power Tools", "0.9.0.0")]
    internal sealed class ViewsForBaseEntitySetsbbd663a39f2c4e8f0688378276dab5f1f199ab4f3ad7575b9497b86695ac1b48 : DbMappingViewCache
    {
        /// <summary>
        /// Gets a hash value computed over the mapping closure.
        /// </summary>
        public override string MappingHashValue
        {
            get { return "bbd663a39f2c4e8f0688378276dab5f1f199ab4f3ad7575b9497b86695ac1b48"; }
        }

        /// <summary>
        /// Gets a view corresponding to the specified extent.
        /// </summary>
        /// <param name="extent">The extent.</param>
        /// <returns>The mapping view, or null if the extent is not associated with a mapping view.</returns>
        public override DbMappingView GetView(EntitySetBase extent)
        {
            if (extent == null)
            {
                throw new ArgumentNullException("extent");
            }

            var extentName = extent.EntityContainer.Name + "." + extent.Name;

            if (extentName == "CodeFirstDatabase.Category")
            {
                return GetView0();
            }

            if (extentName == "CodeFirstDatabase.Location")
            {
                return GetView1();
            }

            if (extentName == "CodeFirstDatabase.Review")
            {
                return GetView2();
            }

            if (extentName == "DatabaseContext.Categories")
            {
                return GetView3();
            }

            if (extentName == "DatabaseContext.Locations")
            {
                return GetView4();
            }

            if (extentName == "DatabaseContext.Reviews")
            {
                return GetView5();
            }

            if (extentName == "DatabaseContext.Review_Category")
            {
                return GetView6();
            }

            if (extentName == "DatabaseContext.Review_Location")
            {
                return GetView7();
            }

            return null;
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.Category.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView0()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Category
        [CodeFirstDatabaseSchema.Category](T1.Category_CategoryId, T1.Category_Name)
    FROM (
        SELECT 
            T.CategoryId AS Category_CategoryId, 
            T.Name AS Category_Name, 
            True AS _from0
        FROM DatabaseContext.Categories AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.Location.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView1()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Location
        [CodeFirstDatabaseSchema.Location](T1.Location_LocationId, T1.Location_Name, T1.Location_Address, T1.Location_City, T1.Location_State, T1.Location_Zip, T1.Location_XCoordinate, T1.Location_YCoordinate, T1.Location_placeId)
    FROM (
        SELECT 
            T.LocationId AS Location_LocationId, 
            T.Name AS Location_Name, 
            T.Address AS Location_Address, 
            T.City AS Location_City, 
            T.State AS Location_State, 
            T.Zip AS Location_Zip, 
            T.XCoordinate AS Location_XCoordinate, 
            T.YCoordinate AS Location_YCoordinate, 
            T.placeId AS Location_placeId, 
            True AS _from0
        FROM DatabaseContext.Locations AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.Review.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView2()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Review
        [CodeFirstDatabaseSchema.Review](T4.Review_ReviewId, T4.Review_ReviewText, T4.Review_Author, T4.Review_Stars, T4.[Review.Category_CategoryId], T4.[Review.Location_LocationId])
    FROM (
        SELECT T1.Review_ReviewId, T1.Review_ReviewText, T1.Review_Author, T1.Review_Stars, T2.[Review.Category_CategoryId], T3.[Review.Location_LocationId], T1._from0, (T2._from1 AND T2._from1 IS NOT NULL) AS _from1, (T3._from2 AND T3._from2 IS NOT NULL) AS _from2
        FROM  (
            SELECT 
                T.ReviewId AS Review_ReviewId, 
                T.ReviewText AS Review_ReviewText, 
                T.Author AS Review_Author, 
                T.Stars AS Review_Stars, 
                True AS _from0
            FROM DatabaseContext.Reviews AS T) AS T1
            LEFT OUTER JOIN (
            SELECT 
                Key(T.Review_Category_Source).ReviewId AS Review_ReviewId, 
                Key(T.Review_Category_Target).CategoryId AS [Review.Category_CategoryId], 
                True AS _from1
            FROM DatabaseContext.Review_Category AS T) AS T2
            ON T1.Review_ReviewId = T2.Review_ReviewId
            LEFT OUTER JOIN (
            SELECT 
                Key(T.Review_Location_Source).ReviewId AS Review_ReviewId, 
                Key(T.Review_Location_Target).LocationId AS [Review.Location_LocationId], 
                True AS _from2
            FROM DatabaseContext.Review_Location AS T) AS T3
            ON T1.Review_ReviewId = T3.Review_ReviewId
    ) AS T4");
        }

        /// <summary>
        /// Gets the view for DatabaseContext.Categories.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView3()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Categories
        [ReviewsJoy.DAL.Category](T1.Category_CategoryId, T1.Category_Name)
    FROM (
        SELECT 
            T.CategoryId AS Category_CategoryId, 
            T.Name AS Category_Name, 
            True AS _from0
        FROM CodeFirstDatabase.Category AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for DatabaseContext.Locations.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView4()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Locations
        [ReviewsJoy.DAL.Location](T1.Location_LocationId, T1.Location_Name, T1.Location_Address, T1.Location_City, T1.Location_State, T1.Location_Zip, T1.Location_XCoordinate, T1.Location_YCoordinate, T1.Location_placeId)
    FROM (
        SELECT 
            T.LocationId AS Location_LocationId, 
            T.Name AS Location_Name, 
            T.Address AS Location_Address, 
            T.City AS Location_City, 
            T.State AS Location_State, 
            T.Zip AS Location_Zip, 
            T.XCoordinate AS Location_XCoordinate, 
            T.YCoordinate AS Location_YCoordinate, 
            T.placeId AS Location_placeId, 
            True AS _from0
        FROM CodeFirstDatabase.Location AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for DatabaseContext.Reviews.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView5()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Reviews
        [ReviewsJoy.DAL.Review](T1.Review_ReviewId, T1.Review_ReviewText, T1.Review_Author, T1.Review_Stars) WITH 
        RELATIONSHIP(CREATEREF(DatabaseContext.Categories, ROW(T1.[Review_Category.Review_Category_Target.CategoryId]),[ReviewsJoy.DAL.Category]),[ReviewsJoy.DAL.Review_Category],Review_Category_Source,Review_Category_Target) 
        RELATIONSHIP(CREATEREF(DatabaseContext.Locations, ROW(T1.[Review_Location.Review_Location_Target.LocationId]),[ReviewsJoy.DAL.Location]),[ReviewsJoy.DAL.Review_Location],Review_Location_Source,Review_Location_Target) 
    FROM (
        SELECT 
            T.ReviewId AS Review_ReviewId, 
            T.ReviewText AS Review_ReviewText, 
            T.Author AS Review_Author, 
            T.Stars AS Review_Stars, 
            True AS _from0, 
            T.Category_CategoryId AS [Review_Category.Review_Category_Target.CategoryId], 
            T.Location_LocationId AS [Review_Location.Review_Location_Target.LocationId]
        FROM CodeFirstDatabase.Review AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for DatabaseContext.Review_Category.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView6()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Review_Category
        [ReviewsJoy.DAL.Review_Category](T3.[Review_Category.Review_Category_Source], T3.[Review_Category.Review_Category_Target])
    FROM (
        SELECT -- Constructing Review_Category_Source
            CreateRef(DatabaseContext.Reviews, row(T2.[Review_Category.Review_Category_Source.ReviewId]), [ReviewsJoy.DAL.Review]) AS [Review_Category.Review_Category_Source], 
            T2.[Review_Category.Review_Category_Target]
        FROM (
            SELECT -- Constructing Review_Category_Target
                T1.[Review_Category.Review_Category_Source.ReviewId], 
                CreateRef(DatabaseContext.Categories, row(T1.[Review_Category.Review_Category_Target.CategoryId]), [ReviewsJoy.DAL.Category]) AS [Review_Category.Review_Category_Target]
            FROM (
                SELECT 
                    T.ReviewId AS [Review_Category.Review_Category_Source.ReviewId], 
                    T.Category_CategoryId AS [Review_Category.Review_Category_Target.CategoryId], 
                    True AS _from0
                FROM CodeFirstDatabase.Review AS T
                WHERE T.Category_CategoryId IS NOT NULL
            ) AS T1
        ) AS T2
    ) AS T3");
        }

        /// <summary>
        /// Gets the view for DatabaseContext.Review_Location.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView7()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Review_Location
        [ReviewsJoy.DAL.Review_Location](T3.[Review_Location.Review_Location_Source], T3.[Review_Location.Review_Location_Target])
    FROM (
        SELECT -- Constructing Review_Location_Source
            CreateRef(DatabaseContext.Reviews, row(T2.[Review_Location.Review_Location_Source.ReviewId]), [ReviewsJoy.DAL.Review]) AS [Review_Location.Review_Location_Source], 
            T2.[Review_Location.Review_Location_Target]
        FROM (
            SELECT -- Constructing Review_Location_Target
                T1.[Review_Location.Review_Location_Source.ReviewId], 
                CreateRef(DatabaseContext.Locations, row(T1.[Review_Location.Review_Location_Target.LocationId]), [ReviewsJoy.DAL.Location]) AS [Review_Location.Review_Location_Target]
            FROM (
                SELECT 
                    T.ReviewId AS [Review_Location.Review_Location_Source.ReviewId], 
                    T.Location_LocationId AS [Review_Location.Review_Location_Target.LocationId], 
                    True AS _from0
                FROM CodeFirstDatabase.Review AS T
                WHERE T.Location_LocationId IS NOT NULL
            ) AS T1
        ) AS T2
    ) AS T3");
        }
    }
}