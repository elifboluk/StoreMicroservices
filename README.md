# FinalStoreMicroservices

"StoreMicroservices" Asp.Net Core 6 ve Microservice mimari ile geliştirilmiş bir Store projesidir.
IdentityServer4 ile kimliklendirme ve yetkilendirme yapısı inşa edilmiştir. 
Veritabanı olarak MongoDb. MSSQL Server ve Redis kullanılmıştır. Veritabanları container(portainer.io) olarak ayağa kaldırılmıştır.
API’nin dışardan kullanabilmesi için token yapısı mevcuttur. Access Token ile microservice'ler korunmaktadır. OAuth 2.0 / OpenID Connect protokollerine uygun yapılı token üretilmektedir. 

