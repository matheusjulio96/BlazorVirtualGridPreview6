using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BlazorVirtualGridComponent
{
    internal static class DelegateExtensions
    {
        public static T CreateDelegate<T>(this MethodInfo method) where T : Delegate
            => (T)method.CreateDelegate(typeof(T));
    }

    internal static class QueryableExtensions
    {
        private static LambdaExpression GetPropertyExpression(Type type, string property)
        {
            var arg = Expression.Parameter(type);
            var expr = property.Split('.')
                .Aggregate((Expression)arg, Expression.Property);
            return Expression.Lambda(expr, arg);
        }

        //public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string keyProperty)
        //    => OrderBy(nameof(_OrderBy), source, GetPropertyExpression(typeof(T), keyProperty));

        class Pet
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public static IOrderedQueryable<T> OrderByM<T>(this IQueryable<T> source, string OrderByClause)
        {
            string[] splitted = OrderByClause.Split(' ');

            if (splitted.Length == 1)
            {
                return OrderBy(nameof(_OrderBy), source, GetPropertyExpression(typeof(T), OrderByClause));
            }
            else
            {
                OrderByClause = splitted[0].Trim(' ');
                return OrderBy(nameof(_OrderByDescending), source, GetPropertyExpression(typeof(T), OrderByClause));
            }
            
        }

        private static IOrderedQueryable<T> _OrderBy<T, TKey>(IQueryable<T> source, LambdaExpression keySelector)
            => source.OrderBy((Expression<Func<T, TKey>>)keySelector);

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string keyProperty)
            => OrderBy(nameof(_OrderByDescending), source, GetPropertyExpression(typeof(T), keyProperty));

        private static IOrderedQueryable<T> _OrderByDescending<T, TKey>(IQueryable<T> source, LambdaExpression keySelector)
            => source.OrderByDescending((Expression<Func<T, TKey>>)keySelector);

        private static IOrderedQueryable<T> OrderBy<T>(string funcName, IQueryable<T> source, LambdaExpression keySelector)
            => typeof(QueryableExtensions).GetMethod(funcName, BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(typeof(T), keySelector.Body.Type)
                .CreateDelegate<Func<IQueryable<T>, LambdaExpression, IOrderedQueryable<T>>>()
                .Invoke(source, keySelector);
    }

    //public static class extensionmethods
    //{
    //    public static IOrderedQueryable<T> OrderBy<T>(
    //        this IQueryable<T> source,
    //        string property)
    //    {
    //        return ApplyOrder<T>(source, property, "OrderBy");
    //    }

    //    public static IOrderedQueryable<T> OrderByDescending<T>(
    //        this IQueryable<T> source,
    //        string property)
    //    {
    //        return ApplyOrder<T>(source, property, "OrderByDescending");
    //    }

    //    public static IOrderedQueryable<T> ThenBy<T>(
    //        this IOrderedQueryable<T> source,
    //        string property)
    //    {
    //        return ApplyOrder<T>(source, property, "ThenBy");
    //    }

    //    public static IOrderedQueryable<T> ThenByDescending<T>(
    //        this IOrderedQueryable<T> source,
    //        string property)
    //    {
    //        return ApplyOrder<T>(source, property, "ThenByDescending");
    //    }

    //class Pet
    //{
    //    public string Name { get; set; }
    //    public int Age { get; set; }
    //}

    //static IOrderedQueryable<T> ApplyOrder<T>(
    //    IQueryable<T> source,
    //    string property,
    //    string methodName)
    //{

    //    string[] props = property.Split('.');
    //    Type type = typeof(T);
    //    ParameterExpression arg = Expression.Parameter(type, "x");
    //    Expression expr = arg;
    //    foreach (string prop in props)
    //    {
    //        // use reflection (not ComponentModel) to mirror LINQ
    //        PropertyInfo pi = type.GetProperty(prop);
    //        expr = Expression.Property(expr, pi);
    //        type = pi.PropertyType;
    //    }
    //    Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
    //    LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

    //    var methods = typeof(Queryable).GetMethods();

    //    Pet[] pets = { new Pet { Name="Barley", Age=8 },
    //               new Pet { Name="Boots", Age=4 },
    //               new Pet { Name="Whiskers", Age=1 } };

    //    IEnumerable<Pet> query = new List<Pet>();

    //    // Sort the Pet objects in the array by Pet.Age.
    //    if (methodName == "OrderBy")
    //    {
    //        query = pets.AsQueryable().OrderBy(pet => pet.Age);
    //    }
    //    else
    //    {
    //        Console.WriteLine("ApplyOrder:OrderByDescending");
    //        query = pets.AsQueryable().OrderByDescending(pet => pet.Age); // fazer este funcionar
    //    }

    //    foreach (Pet pet in query)
    //        Console.WriteLine("{0} - {1}", pet.Name, pet.Age);

    //    foreach (var mmethod in methods)
    //    {
    //        Console.WriteLine(mmethod.Name);
    //    }

    //    var tmethod = methods.Single(
    //            method => method.Name == methodName
    //                    && method.IsGenericMethodDefinition
    //                    && method.GetGenericArguments().Length == 2
    //                    && method.GetParameters().Length == 2);

    //    var result = (IOrderedQueryable<T>)tmethod.MakeGenericMethod(typeof(T), type)
    //            .Invoke(null, new object[] { source, lambda });

    //    var resultList = result.ToList();

    //    Console.WriteLine(resultList[0]);

    //    return result;
    //}





    //public static IQueryable<T> OrderBy<T>(this IQueryable<T> source,
    //string property,
    //bool asc = true)
    //{
    //    //STEP 1: Verify the property is valid
    //    var searchProperty = typeof(T).GetProperty(property);
    //    Console.WriteLine(searchProperty);

    //    if (searchProperty == null)
    //        throw new ArgumentException("property");

    //    if (!searchProperty.PropertyType.IsValueType &&
    //        !searchProperty.PropertyType.IsPrimitive &&
    //        !searchProperty.PropertyType.Namespace.StartsWith("System") &&
    //        !searchProperty.PropertyType.IsEnum)
    //        throw new ArgumentException("property");

    //    if (searchProperty.GetMethod == null ||
    //        !searchProperty.GetMethod.IsPublic)
    //        throw new ArgumentException("property");

    //    //STEP 2: Create the OrderBy property selector
    //    var parameter = Expression.Parameter(typeof(T), "o");
    //    var selectorExpr = Expression.Lambda(
    //            Expression.Property(parameter, property), parameter);

    //    //STEP 3: Update the IQueryable expression to include OrderBy
    //    Expression queryExpr = source.Expression;
    //    queryExpr = Expression.Call(
    //        typeof(Queryable),
    //        asc ? "OrderBy" : "OrderByDescending",
    //        new Type[] {
    //    source.ElementType,
    //    searchProperty.PropertyType },
    //        queryExpr,
    //        selectorExpr);

    //    return source.Provider.CreateQuery<T>(queryExpr);
    //}
    //}
}
