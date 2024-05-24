Single Server senaryosu için kullanılan ConcurrentDictionary'de dataları cache etme çözümü uzun vadede sorun çıkartmadan çalışacak bir çözümdür. Ancak big-data girişi yapılması durumunda yüksek memory kullanımına yol açması mümkündür, bahsedilen big-data case'i ise real-world example olarak bakılınca pek mümkün görünmeyeceği için bu çözüm tercih edilmiştir. ConcurrentBag yerine ConcurrentDictionary kullanılmasın sebebi ise data kontrolü sağlanırken time-complexity'i O(1) -o constant- olarak tutmaktır.
