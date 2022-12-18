# Site Management

Site Management, site yöneticilerinin hayatlarını kolaylaştırmayı hedefleyen bir projedir. Sitelerde yer alan dairelerin aidat ve ortak kullanım elektrik, su ve doğalgaz faturalarının yönetimini bir sistem üzerinden mümkün kılmaktadır. Projenin içerisinde iki farklı kullanıcı tipi bulunmakta ve proje bu kullanıcı tiplerine göre belli özellikler barındırmakta. 
 
## :pouting_man: Kullanıcı Tipleri
### 1-Admin/Yönetici Rolü 

**Daire İşlemleri:** Daire bilgilerini girer, listeler, düzenler ve siler. İkamet eden kullanıcı bilgilerini girer.

**Mesaj İşlemleri:** Tüm mesajlaşma işlemlerini gerçekleştirir.

**Kullanıcı İşlemleri:** Kişileri listeler, düzenler, siler.

**Ödeme İşlemleri:** Daire başına ödenmesi gereken aidat ve fatura bilgilerini girer. Toplu veya tek tek atama yapar. Gelen ödeme bilgileri ve borç-alacak listesini görür. Otomatik olarak fatura ödemeyen kişilere günlük mail atılır.


### 2- Kullanıcı Rolü

- Kendisine atanan fatura ve aidat bilgilerini görür.
- Sadece kredi kartı ile ödeme yapabilir.
- Yöneticiye mesaj gönderebilir.


## :computer: Projenin Kurulumu ve Çalıştırması
-MongoDb, Sql Server bilgisayarınız yüklü olmalı.

-Veritabanını oluşturmak için; Projeyi açıp set as start up project olarak SiteManagement.API olarak belirleyin. Daha sonra package manager console’unda varsayılan proje olarak SiteManagement.Infrastructure seçin ve update-database komutunu girin.

-Projeyi çalıştırmak için solution ayarlarından Multiple Startup Project olarak PaymentManagement.API ve SiteManagement.API projelerini seçin. İki projenin aynı anda çalışabilmesi için port bilgilerinizi güncelleyin.

<h2> 🛠 &nbsp;Kullanılan Teknolojiler</h2>
ASP.Net 5, MongoDb, Sql Server, Entity Framework, Identity, Fluent Validation, AutoMapper, Hangfire, IMemoryCache
    
