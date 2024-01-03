
## **Attributes And Dependency Injection(Öznitelik ve Bağımlılık Enjeksiyonu)**

### Özet:
Bu proje içerisinde Attributes ile bir bağımlılıklarımızı nasıl daha kolay yönetebileceğimize dair kısa bir örnek yaptık.

### Problem: 
Hepimiz .net bize sunduğu en güzel nimetlerinden '*Microsoft.Extensions.DependencyInjection*' aktif bir şekilde kullanıyoruz, ilk başlarda bir kaç satırlık olan bağımlılıklar bir süre sonra iki üç basamakları satırlar haline geldiğini görüyoruz, örnek projemizde bu problemden kurtulmanıza yarayacak basit bir örnek verilmiştir.

### Çözüm - Mantık
Projemiz içerisinde arabirimlerimizi uygulayan sıflarımızı '*IServiceCollection*' yani *DependencyInjection* tanıtmamız gereklidir, bu süreci genellikle (istisna durumlar hariç)manuel şekilde yaparız.

İlk olarak DI(Bundan sonraki süreçte DependencyInjection kısaltması olarak kullanacağım) içerisine kayıt edeceğimiz arabirim ve bu arabirimi uygulayan sınıflarımızı etiketleyebileceğimiz bir şey olmalı, yani bir class library var ve bu library içerisinden DI tanımlama yapacağımız sınıfları elde etmemiz gerekli ki yazılım sürecimize başlayalım.

Bunun için Attribute(Öznitelik) den yararlanacağız.

    public class DependencyRegisterAttribute : Attribute
    {
        public Type InterfaceType {  get; set; }
        public DependencyTypes DependencyTypes { get; set; }
    
        public DependencyRegisterAttribute(Type ınterfaceType, DependencyTypes dependencyTypes)
        {
            this.InterfaceType = ınterfaceType;
            DependencyTypes = dependencyTypes;
        }
    }

Attrubtesi kullandığımız sınıfımıza gelelim

    [DependencyRegister(typeof(IProductService),DependencyTypes.Scopet)]
    public class ProductService : IProductService


Yukarıdaki örnekte gördüğümüz gibi iki durumu belirtiyoruz, sınıfımızı DI hangi arayüz ile tanıtacağız ve kayıt tipimiz.

Ardından basit bir Extension methodumuz sayesinde özel öz niteliğimizi kullanan sınıflarımıza ulaşıyoruz ve bu sayede ihtiyacımız olan diğer tüm özelliklere erişiyoruz.
public static class ServiceRegistration
{

        public static void ApplicationServiceInitialize(this IServiceCollection serviceDescriptors)
        {
            Assembly.GetExecutingAssembly().GetTypes()?.Where(x => x.GetCustomAttribute<DependencyRegisterAttribute>() != null).ToList()?.ForEach(classType =>
            {
                DependencyRegisterAttribute dependencyRegisterAttribute = classType.GetCustomAttribute<DependencyRegisterAttribute>();
                Type interfaceType = classType.GetInterface(dependencyRegisterAttribute.InterfaceType.Name);
    
                if (interfaceType == null)
                    throw new InterfaceNotFoundException(classType.Name,dependencyRegisterAttribute.InterfaceType.Name);
    
                if (dependencyRegisterAttribute.DependencyTypes == DependencyTypes.Scopet)
                {
                    serviceDescriptors.AddScoped(interfaceType,classType);
                }else if(dependencyRegisterAttribute.DependencyTypes == DependencyTypes.Transient)
                {
                    serviceDescriptors.AddTransient(interfaceType,classType);
                }else if (dependencyRegisterAttribute.DependencyTypes == DependencyTypes.Singleton)
                {
                    serviceDescriptors.AddSingleton(interfaceType,classType);
                }else
                {
                    serviceDescriptors.AddScoped(interfaceType,classType);
                }
    
            });
        }
    }
