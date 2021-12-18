namespace WeLoveBooks.Mvc.Services.ObjectToModelConverter;

public interface IObjectToModelConverter<T, U> where T : class where U : class
{
    U Convert(T obj);
}
