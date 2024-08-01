//_characters: Kodları oluşturmak için kullanılacak karakter kümesi. Bu küme, kodların tahmin edilmesini zorlaştırmak için harfler ve sayılar içerir.
//_codeLength: Üretilen kodların uzunluğunu tanımlar. Bu durumda her kod 8 karakter uzunluğundadır.
//_generatedCodes: Üretilen kodların benzersiz olmasını sağlamak için kullanılan bir HashSet. Bu set, aynı kodun birden fazla kez üretilmesini önler.
//rng: Güvenli rastgele sayılar üretmek için kullanılan RandomNumberGenerator nesnesi.

//Main Metodu
//numberOfCodes: Üretilmesi gereken kod sayısı.Bu örnekte 1000 kod üretilecek.
//GenerateCode: Kod üretmek için çağrılan metod.
//Console.WriteLine(code): Üretilen her kodu konsola yazdırır.
//testCode: Geçerliliği test edilecek örnek kod.
//CheckCode(testCode): Kodun geçerliliğini kontrol eder ve sonucu isValid değişkenine atar.
//Console.WriteLine(...): Örnek kodun geçerli olup olmadığını konsola yazdırır.
//Console.ReadKey(): Konsolun kapanmasını önler ve kullanıcıdan bir tuşa basmasını bekler.

//GenerateCode()
//char[] codeChars = new char[_codeLength]: Kod karakterlerini depolamak için bir karakter dizisi oluşturur.
//for (int i = 0; i < _codeLength - 1; i++): İlk 7 karakteri rastgele seçer.
//codeChars[i] = _characters[GetRandomInt(_characters.Length)]: Karakter kümesinden rastgele bir karakter seçer.
//codeChars[_codeLength - 1] = CalculateCheckCharacter(codeChars): Son karakteri kontrol karakteri olarak hesaplar.
//new string(codeChars): Karakter dizisini string'e dönüştürür.
//while (!CheckCode(code) || !_generatedCodes.Add(code)): Kodun geçerli olup olmadığını ve benzersiz olup olmadığını kontrol eder. Geçerli ve benzersiz değilse, yeni bir kod üretir.

//CalculateCheckCharacter
//int hash = 0: Hash hesaplaması için başlangıç değeri.
//for (int i = 0; i < _codeLength - 1; i++): İlk 7 karakteri iteratif olarak işler.
//hash = (hash * 31 + codeChars[i]) % _characters.Length: Mevcut karakteri kullanarak hash hesaplaması yapar. 31 sabiti hash dağılımını iyileştirmek için kullanılır.
//return _characters[hash]: Hesaplanan hash değerine karşılık gelen karakteri döndürür.

//CheckCode
//if (code.Length != _codeLength) return false;: Kod uzunluğunu kontrol eder. Eğer uzunluk istenen uzunlukta değilse, false döner.
//if (code.Any(c => !_characters.Contains(c))) return false;: Her karakterin geçerli karakter kümesinde olup olmadığını kontrol eder.
//char[] codeChars = code.Substring(0, _codeLength - 1).ToCharArray();: İlk 7 karakteri alır ve bir karakter dizisine dönüştürür.
//char expectedCheckCharacter = CalculateCheckCharacter(codeChars);: İlk 7 karakteri kullanarak beklenen kontrol karakterini hesaplar.
//return code[_codeLength - 1] == expectedCheckCharacter;: Hesaplanan kontrol karakteri ile kodun son karakterini karşılaştırır. Eğer eşleşiyorlarsa, true döner.

//GetRandomInt
//byte[] randomNumber = new byte[4];: Rastgele baytları depolamak için bir bayt dizisi oluşturur.
//rng.GetBytes(randomNumber);: Rastgele baytları üretir ve randomNumber dizisine doldurur.
//Math.Abs(BitConverter.ToInt32(randomNumber, 0)) % max;: Baytları bir tam sayıya dönüştürür ve belirtilen aralıkta bir rastgele sayı elde etmek için mod işlemi yapar. Elde edilen sonuç, max değeri ile sınırlıdır ve negatif değerler önlenir.