Distributed systems senaryosu için sunulan Redis çözümünün en büyük zayıf yönü eğer redis sunucusu farklı bir lokasyonda bulunursa yaşanabilecek gecikme sorunlarıdır. Redis yerine farklı veritabanları kullanılabilir ancak verilen case'yi eğer real-world olarak düşünürsek in-memory databaseler üzerinden gitmek her açıdan daha mantıklı olacaktır. Bunun yanında redis eğer başarı bir şekilde connect olmazsa ortaya farklı sorunlar doğabilir. Bunun önüne geçilmek için hem local caching hem de in-memory caching yapılması çok daha doğru olacaktır, ancak iki farklı çözüm istediği için bu yoldan gitmek durumunda kaldım. Eğer gerçek bir aplikasyon oluşturacak olsaydım hem local caching/locking hem de redis kullanırdım, izleyeceğim flow ise;

Local Cache'de kontrol et
  - Eğer varsa
      - Result olarak false ver
  - Eğer yoksa
      - Redis üzerinden kontrol sağla
        - Eğer varsa
          - Result olarak false ver
        - Eğer yoksa
          - Başarılı şekilde ekle

Bu yol dahilinde her ekleme yapılmak istediğinde ilgili slave node hem kendi local cache'inde hem de ortak in-memory db'de kontrol sağlayacak ve çok daha optimize bir yol izlenecektir.
