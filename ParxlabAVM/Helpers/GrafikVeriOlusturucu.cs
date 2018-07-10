﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParxlabAVM.Models;

namespace ParxlabAVM.Helpers
{
    public class GrafikVeriOlusturucu
    {

        public List<ZamanAraligiVerisi> ZamanDilimindeGirenArac(int parkId, DateTime baslangic, DateTime bitis, int aralik)
        {
            /*
            * Bu fonksiyon verilen zaman aralığında id'si verilen parka giren araç sayısının grafiği çizilmesi için kullanılır
            * baslangic ve bitis değerlerini aralik ile verilen zaman dilimlerine böler
            * aralik'in türü saniyedir, saatte 3600, günde 86400, haftada 604800 saniye vardır
            * Fonksiyon her bir dilimde giren araç sayısını, dilimin başlangıç ve bitiş zamanı ile birlikte bir ZamanAraligiVerisi nesnesine koyar
            * Tüm dilimlerin ZamanAraligiVerisi nesnesini bir liste içinde döndürür
            * Bu fonksiyon saatlere, günlere ve haftalara göre giren araç sayılarını bulmak için kullanılmalıdır
            * Ayların ve Yılların içindeki saniye sayısı eşit olmadığı için onlar için kendi fonksiyonları hazırlanmıştır
            * 
            */
            DateTime dilimBasi, dilimSonu;
            Model veritabani = new Model();
            List<ZamanAraligiVerisi> sonuc = new List<ZamanAraligiVerisi>();
            IQueryable<anatablo> araliktakiTumVeriler = (from veri in veritabani.anatablo
                                                         where (veri.parkid == parkId && DateTime.Compare(baslangic, (DateTime)veri.giriszamani) <= 0
                                                         && (DateTime.Compare(bitis, (DateTime)veri.giriszamani) >= 0))
                                                         select veri);
            //Baslangic ve bitis tarihlerinde giris yapanlar da dahil aralıktaki tüm verileri al

            dilimBasi = baslangic;
            while (DateTime.Compare(dilimBasi, bitis) < 0)
            {
                dilimSonu = dilimBasi.AddSeconds(aralik);
                sonuc.Add(new ZamanAraligiVerisi
                {
                    Baslangic = dilimBasi,
                    Bitis = dilimSonu,
                    Deger = (from veri in araliktakiTumVeriler
                             where (DateTime.Compare(dilimBasi, (DateTime)veri.giriszamani) <= 0
                             && (DateTime.Compare(dilimSonu, (DateTime)veri.giriszamani) >= 0))
                             select veri).Count()
                });

                dilimBasi = dilimSonu;
            }
            return sonuc;

        }

        public List<ZamanAraligiVerisi> AylaraGöreGirenArac(int parkId, DateTime baslangic, DateTime bitis)
        {
            /*
            * Bu fonksiyon verilen zaman aralığında id'si verilen parka giren araç sayısının grafiği çizilmesi için kullanılır
            * baslangic ve bitis değerlerinin arasındaki her bir ay için giren araç sayılarını bulur
            * Fonksiyon her bir ayda giren araç sayısını, dilimin başlangıç ve bitiş zamanı ile birlikte bir ZamanAraligiVerisi nesnesine koyar
            * Tüm dilimlerin ZamanAraligiVerisi nesnesini bir liste içinde döndürür
            * 
            */
            DateTime dilimBasi, dilimSonu;
            Model veritabani = new Model();
            List<ZamanAraligiVerisi> sonuc = new List<ZamanAraligiVerisi>();
            IQueryable<anatablo> araliktakiTumVeriler = (from veri in veritabani.anatablo
                                                         where (veri.parkid == parkId && DateTime.Compare(baslangic, (DateTime)veri.giriszamani) <= 0
                                                         && (DateTime.Compare(bitis, (DateTime)veri.giriszamani) >= 0))
                                                         select veri);
            //Baslangic ve bitis tarihlerinde giris yapanlar da dahil aralıktaki tüm verileri al

            dilimBasi = baslangic;
            while (DateTime.Compare(dilimBasi, bitis) < 0)
            {
                dilimSonu = dilimBasi.AddMonths(1);
                sonuc.Add(new ZamanAraligiVerisi
                {
                    Baslangic = dilimBasi,
                    Bitis = dilimSonu,
                    Deger = (from veri in araliktakiTumVeriler
                             where (DateTime.Compare(dilimBasi, (DateTime)veri.giriszamani) <= 0
                             && (DateTime.Compare(dilimSonu, (DateTime)veri.giriszamani) >= 0))
                             select veri).Count()
                });

                dilimBasi = dilimSonu;
            }
            return sonuc;

        }

        public List<ZamanAraligiVerisi> YillaraGöreGirenArac(int parkId, DateTime baslangic, DateTime bitis)
        {
            /*
            * Bu fonksiyon verilen zaman aralığında id'si verilen parka giren araç sayısının grafiği çizilmesi için kullanılır
            * baslangic ve bitis değerlerinin arasındaki her bir yıl için giren araç sayılarını bulur
            * Fonksiyon her bir yılda giren araç sayısını, dilimin başlangıç ve bitiş zamanı ile birlikte bir ZamanAraligiVerisi nesnesine koyar
            * Tüm dilimlerin ZamanAraligiVerisi nesnesini bir liste içinde döndürür
            * 
            */
            DateTime dilimBasi, dilimSonu;
            Model veritabani = new Model();
            List<ZamanAraligiVerisi> sonuc = new List<ZamanAraligiVerisi>();
            IQueryable<anatablo> araliktakiTumVeriler = (from veri in veritabani.anatablo
                                                         where (veri.parkid == parkId && DateTime.Compare(baslangic, (DateTime)veri.giriszamani) <= 0
                                                         && (DateTime.Compare(bitis, (DateTime)veri.giriszamani) >= 0))
                                                         select veri);
            //Baslangic ve bitis tarihlerinde giris yapanlar da dahil aralıktaki tüm verileri al

            dilimBasi = baslangic;
            while (DateTime.Compare(dilimBasi, bitis) < 0)
            {
                dilimSonu = dilimBasi.AddYears(1);
                sonuc.Add(new ZamanAraligiVerisi
                {
                    Baslangic = dilimBasi,
                    Bitis = dilimSonu,
                    Deger = (from veri in araliktakiTumVeriler
                             where (DateTime.Compare(dilimBasi, (DateTime)veri.giriszamani) <= 0
                             && (DateTime.Compare(dilimSonu, (DateTime)veri.giriszamani) >= 0))
                             select veri).Count()
                });

                dilimBasi = dilimSonu;
            }
            return sonuc;
        }

        public List<ZamanAraligiVerisi> SaatlereGöreGirenArac(int parkId, DateTime baslangic, DateTime bitis)
        {
            return ZamanDilimindeGirenArac(parkId, baslangic, bitis, 3600);
        }

        public List<ZamanAraligiVerisi> GünlereGöreGirenArac(int parkId, DateTime baslangic, DateTime bitis)
        {
            return ZamanDilimindeGirenArac(parkId, baslangic, bitis, 86400);
        }

        public List<ZamanAraligiVerisi> HaftalaraGöreGirenArac(int parkId, DateTime baslangic, DateTime bitis)
        {
            return ZamanDilimindeGirenArac(parkId, baslangic, bitis, 604800);
        }

        public List<ZamanAraligiVerisi> ZamanDilimindeAraclarınHarcadigiToplamZaman
            (int parkId, DateTime baslangic, DateTime bitis, int aralik)
        {
            /*
            * Bu fonksiyon verilen zaman aralığında id'si verilen parkta araçların geçirdiği toplam zamanın grafiğinin çizilmesi için kullanılır
            * baslangic ve bitis değerlerini aralik ile verilen zaman dilimlerine böler
            * aralik'in türü saniyedir, saatte 3600, günde 86400, haftada 604800 saniye vardır
            * Fonksiyon her bir dilimde parkta geçirilmiş zaman miktarını(saat cinsinden),
            * dilimin başlangıç ve bitiş zamanı ile birlikte bir ZamanAraligiVerisi nesnesine koyar
            * Tüm dilimlerin ZamanAraligiVerisi nesnesini bir liste içinde döndürür
            * Bu fonksiyon saatlere, günlere ve haftalara göre giren araç sayılarını bulmak için kullanılmalıdır
            * Ayların ve Yılların içindeki saniye sayısı eşit olmadığı için onlar için kendi fonksiyonları hazırlanmıştır
            * 
            */
            DateTime dilimBasi, dilimSonu;
            double toplam;
            Model veritabani = new Model();
            List<ZamanAraligiVerisi> sonuc = new List<ZamanAraligiVerisi>();
            IQueryable<anatablo> araliktakiTumVeriler = (from veri in veritabani.anatablo
                                                         where (veri.parkid == parkId
                                                         && (DateTime.Compare(baslangic, (DateTime)veri.giriszamani) <= 0
                                                                && DateTime.Compare(bitis, (DateTime)veri.giriszamani) >= 0)//Giriş zamanı aralığın içinde
                                                         || (DateTime.Compare(baslangic, (DateTime)veri.cikiszamani) <= 0
                                                                && DateTime.Compare(bitis, (DateTime)veri.cikiszamani) >= 0)//çıkış zamanı aralığın içinde
                                                         || (DateTime.Compare(baslangic, (DateTime)veri.giriszamani) >= 0
                                                                && DateTime.Compare(bitis, (DateTime)veri.cikiszamani) <= 0))//Aralıktan önce girip sonra çıkmış
                                                         select veri);
            //Verilen zaman aralığında parkta olan araçları al

            dilimBasi = baslangic;
            while (DateTime.Compare(dilimBasi, bitis) < 0)
            {
                dilimSonu = dilimBasi.AddSeconds(aralik);
                toplam = 0;

                foreach (var item in (from veri in veritabani.anatablo
                                      where (veri.parkid == parkId
                                      && (DateTime.Compare(dilimBasi, (DateTime)veri.giriszamani) <= 0
                                             && DateTime.Compare(dilimSonu, (DateTime)veri.giriszamani) >= 0)//Giriş zamanı aralığın içinde
                                      || (DateTime.Compare(dilimBasi, (DateTime)veri.cikiszamani) <= 0
                                             && DateTime.Compare(dilimSonu, (DateTime)veri.cikiszamani) >= 0)//Çıkış zamanı aralığın içinde
                                      || (DateTime.Compare(dilimBasi, (DateTime)veri.giriszamani) >= 0
                                             && DateTime.Compare(dilimSonu, (DateTime)veri.cikiszamani) <= 0))//Aralıktan önce girip sonra çıkmış
                                      select veri))
                {
                    if (DateTime.Compare((DateTime)item.giriszamani, dilimBasi) < 0 && DateTime.Compare((DateTime)item.cikiszamani, dilimSonu) < 0)
                    {
                        //Dilim Başlangıcından önce girip bitişinden önce çıkmış
                        toplam += ((DateTime)item.cikiszamani).Subtract(dilimBasi).TotalSeconds / 3600.0;
                    }
                    else if (DateTime.Compare((DateTime)item.giriszamani, dilimBasi) >= 0 && DateTime.Compare((DateTime)item.cikiszamani, dilimSonu) < 0)
                    {
                        // Dilimin içinde girip çıkmış
                        toplam += ((DateTime)item.cikiszamani).Subtract(((DateTime)item.giriszamani)).TotalSeconds / 3600.0;
                    }
                    else if (DateTime.Compare((DateTime)item.giriszamani, dilimBasi) >= 0 && DateTime.Compare((DateTime)item.cikiszamani, dilimSonu) >= 0)
                    {
                        // Dilimin içinde girip, dilim bitişinden sonra çıkmış
                        toplam += dilimSonu.Subtract(((DateTime)item.giriszamani)).TotalSeconds / 3600.0;
                    }
                    else
                    {
                        //Dilim başlangıcından önce girip, dilim sonundan sonra çıkmış
                        toplam += aralik / 3600.0;
                    }
                }

                sonuc.Add(new ZamanAraligiVerisi
                {
                    Baslangic = dilimBasi,
                    Bitis = dilimSonu,
                    Deger = toplam
                });

                dilimBasi = dilimSonu;
            }
            return sonuc;

        }

        public List<ZamanAraligiVerisi> SaatlereGoreAraclarınHarcadigiToplamZaman(int parkId, DateTime baslangic, DateTime bitis)
        {
            return ZamanDilimindeAraclarınHarcadigiToplamZaman(parkId, baslangic, bitis, 3600);
        }

        public List<ZamanAraligiVerisi> GunlereGoreAraclarınHarcadigiToplamZaman(int parkId, DateTime baslangic, DateTime bitis)
        {
            return ZamanDilimindeAraclarınHarcadigiToplamZaman(parkId, baslangic, bitis, 86400);
        }

        public List<ZamanAraligiVerisi> HaftalaraGöreAraclarınHarcadigiToplamZaman(int parkId, DateTime baslangic, DateTime bitis)
        {
            return ZamanDilimindeAraclarınHarcadigiToplamZaman(parkId, baslangic, bitis, 604800);
        }

        public List<ZamanAraligiVerisi> AylaraGoreAraclarınHarcadigiToplamZaman(int parkId, DateTime baslangic, DateTime bitis)
        {
            /*
            * Bu fonksiyon verilen zaman aralığında id'si verilen parkta araçların geçirdiği toplam zamanın grafiğinin çizilmesi için kullanılır
            * baslangic ve bitis değerlerini aylara böler
            * Fonksiyon her bir dilimde parkta geçirilmiş zaman miktarını(saat cinsinden),
            * dilimin başlangıç ve bitiş zamanı ile birlikte bir ZamanAraligiVerisi nesnesine koyar
            * Tüm dilimlerin ZamanAraligiVerisi nesnesini bir liste içinde döndürür
            * 
            */
            DateTime dilimBasi, dilimSonu;
            double toplam;
            Model veritabani = new Model();
            List<ZamanAraligiVerisi> sonuc = new List<ZamanAraligiVerisi>();
            IQueryable<anatablo> araliktakiTumVeriler = (from veri in veritabani.anatablo
                                                         where (veri.parkid == parkId
                                                         && (DateTime.Compare(baslangic, (DateTime)veri.giriszamani) <= 0
                                                                && DateTime.Compare(bitis, (DateTime)veri.giriszamani) >= 0)//Giriş zamanı aralığın içinde
                                                         || (DateTime.Compare(baslangic, (DateTime)veri.cikiszamani) <= 0
                                                                && DateTime.Compare(bitis, (DateTime)veri.cikiszamani) >= 0)//çıkış zamanı aralığın içinde
                                                         || (DateTime.Compare(baslangic, (DateTime)veri.giriszamani) >= 0
                                                                && DateTime.Compare(bitis, (DateTime)veri.cikiszamani) <= 0))//Aralıktan önce girip sonra çıkmış
                                                         select veri);
            //Verilen zaman aralığında parkta olan araçları al

            dilimBasi = baslangic;
            while (DateTime.Compare(dilimBasi, bitis) < 0)
            {
                dilimSonu = dilimBasi.AddMonths(1);
                toplam = 0;

                foreach (var item in (from veri in veritabani.anatablo
                                      where (veri.parkid == parkId
                                      && (DateTime.Compare(dilimBasi, (DateTime)veri.giriszamani) <= 0
                                             && DateTime.Compare(dilimSonu, (DateTime)veri.giriszamani) >= 0)//Giriş zamanı aralığın içinde
                                      || (DateTime.Compare(dilimBasi, (DateTime)veri.cikiszamani) <= 0
                                             && DateTime.Compare(dilimSonu, (DateTime)veri.cikiszamani) >= 0)//Çıkış zamanı aralığın içinde
                                      || (DateTime.Compare(dilimBasi, (DateTime)veri.giriszamani) >= 0
                                             && DateTime.Compare(dilimSonu, (DateTime)veri.cikiszamani) <= 0))//Aralıktan önce girip sonra çıkmış
                                      select veri))
                {
                    if (DateTime.Compare((DateTime)item.giriszamani, dilimBasi) < 0 && DateTime.Compare((DateTime)item.cikiszamani, dilimSonu) < 0)
                    {
                        //Dilim Başlangıcından önce girip bitişinden önce çıkmış
                        toplam += ((DateTime)item.cikiszamani).Subtract(dilimBasi).TotalSeconds / 3600.0;
                    }
                    else if (DateTime.Compare((DateTime)item.giriszamani, dilimBasi) >= 0 && DateTime.Compare((DateTime)item.cikiszamani, dilimSonu) < 0)
                    {
                        // Dilimin içinde girip çıkmış
                        toplam += ((DateTime)item.cikiszamani).Subtract(((DateTime)item.giriszamani)).TotalSeconds / 3600.0;
                    }
                    else if (DateTime.Compare((DateTime)item.giriszamani, dilimBasi) >= 0 && DateTime.Compare((DateTime)item.cikiszamani, dilimSonu) >= 0)
                    {
                        // Dilimin içinde girip, dilim bitişinden sonra çıkmış
                        toplam += dilimSonu.Subtract(((DateTime)item.giriszamani)).TotalSeconds / 3600.0;
                    }
                    else
                    {
                        //Dilim başlangıcından önce girip, dilim sonundan sonra çıkmış
                        toplam += dilimSonu.Subtract(dilimBasi).TotalSeconds / 3600.0;
                    }
                }

                sonuc.Add(new ZamanAraligiVerisi
                {
                    Baslangic = dilimBasi,
                    Bitis = dilimSonu,
                    Deger = toplam
                });

                dilimBasi = dilimSonu;
            }
            return sonuc;

        }


        public List<ZamanAraligiVerisi> OrtalamaBul(int sonBoyut, List<ZamanAraligiVerisi> veriler)
        {
            /*sonBoyut boyutunda bir ZamanAraligiVerisi listesi döndürür
             * Haftanın içinde günlerin, gün içinde saatlerin vs bir zaman aralığında ortalamasını dödürmek için kullanılır
             * sonuc listesinin her bir indeksi için:
             * veriler listesinde mod(sonBoyut) indeksinde bulunan tüm verilerin değerlerinin ortalamasını
             * ve ilk karşılaşılan indexlerdeki zaman verisini atar
             * Örneğin haftadaki günlerin bir zaman aralığındaki ortalamasını bulmak için
             * (örneğin bir yıl içinde pzt ortalaması 3560, salı ortalaması 4780, pazar ortalaması 7346 vs.)
             * sonBoyut olarak 7
             * veriler olarak da günlere göre hesap yapan fonksiyonlardan (GünlereGöreGirenArac,GünlereGöreAraclarınHarcadigiToplamZaman)
             * birinin sonucu kullanılmalıdır
             * 
             */
            List<ZamanAraligiVerisi> sonuc = new List<ZamanAraligiVerisi>(sonBoyut);
            int[] tur = new int[sonBoyut];  //Ortalamayı bulmak için toplam'ı böleceğimiz sayılar
            int ilkBoyut = veriler.Count();

            for (int i = 0; i < sonBoyut; i++)
            {
                sonuc.Add(veriler[i]); //İlk verilerin zamanları sonuca atanıyor ve içlerindeki değerler toplamlara ekleniyor
                tur[i] = 1; //Tüm indexlere bir defa sayı eklendi

            }
            for (int i = sonBoyut; i < ilkBoyut; i++)//İlk sonBoyut sayısında girdiyi zaten inceledik önceki döngüde
            {
                sonuc[i % sonBoyut].Deger += veriler[i].Deger; //Toplamlar Deger'de tutuluyor
                tur[i % sonBoyut]++;
            }
            for (int i = 0; i < sonBoyut; i++)
            {
                sonuc[i].Deger /= tur[i];
            }

            return sonuc;
        }
        
        public string GrafikVeriEtiketiOlustur(ZamanAraligiVerisi dilim, int degisendenOncekilerinSayisi,
                                                int degisendenSonrkilerinSayisi, bool ikinciTarihiGoster)
        {
            /* Bu fonksiyon grafik çizilirken alt tarafa yazılacak tarihlerin belirlenmesi için kullanılır
             * İki tarih arasındaki tek fark yalnızca ayın hangi günü olduklarıysa yıl verisini, saat ve dakikayı da yazmaya gerek yok
             * Tarihlerin arasındaki farklı en büyük kısmı bulmak için önce tarih çıkarması yapılır
             * Farkın içinde sırayla yıl, ay, gün, saat ve dakika sayısına bakılır.
             * {"yyyy","MM","dd","HH","mm"} dizesinde, farkın olduğu yerin indeksi "int degisen" değişkeninde tutulur
             * degisen, ondan önce "degisendenOncekilerinSayisi" ve sonra "degisendenSonrkilerinSayisi" sayısında indekste bulunanlar
             * yeni bir listeye alınır
             * Bu listeden DateTime.toString fonksiyonunda kullanılmak üzere bir string oluşturulur
             * Fonksiyon dd/MM/yyyy HH:ss formatıyla çağrılacak, o yüzden daha önce oluşturulan listedeki elemanlar
             * bu formata uyacak şekilde string'e ekleniyor.
             * sonuç stringi Datetime.toString fonksiyonuyla belirleniyor, eğer ikinciTarihiGoster doğruysa ikinci tarih için de
             * bu fonksiyon kullanılıp sonuçları birleştirip döndürülüyor.
             */
            int degisen;
            string[] tamFormat = { "yyyy", "MM", "dd", "HH", "mm" };
            string[] yeniFormat;
            string formatStringi = "";
            TimeSpan fark = dilim.Bitis.Subtract(dilim.Baslangic);
            if (fark.TotalDays>=365) //Arada yıl var
            {
                degisen = 0;
            }
            else if (fark.TotalDays >= 30) //Arada ay var
            {
                degisen = 1;
            }
            else if (fark.TotalDays >= 1) //Arada gün var
            {
                degisen = 2;
            }
            else if (fark.TotalHours >= 1) //Arada saat var
            {
                degisen = 3;
            }
            else
            {
                degisen = 4;
            }

            yeniFormat = ArrayKesiti(tamFormat, 0 > degisen - degisendenOncekilerinSayisi ? 0 : degisen - degisendenOncekilerinSayisi,
                tamFormat.Length < degisendenSonrkilerinSayisi - degisendenOncekilerinSayisi + 1 ? tamFormat.Length : degisendenSonrkilerinSayisi - degisendenOncekilerinSayisi + 1);
            //Tam formatın neresinden neresine alınacağı belirlendi, ve kesit alındı

            foreach (var str in yeniFormat)
            {
                switch (str)
                {
                    case ("dd"):
                        formatStringi += "dd";
                        break;
                    case ("MM"):
                        formatStringi += formatStringi.Length == 0 ? "MM" : "/" + "MM";
                        break;
                    case ("yyyy"):
                        formatStringi += formatStringi.Length == 0 ? "yyyy" : "/" + "yyyy";
                        break;
                    case ("HH"):
                        formatStringi += formatStringi.Length == 0 ? "HH" : " " + "HH";
                        break;
                    case ("mm"):
                        formatStringi += formatStringi.Length == 0 ? "mm" : ":" + "mm";
                        break;
                    default:
                        break;
                }
            }
                return ikinciTarihiGoster ? dilim.Baslangic.ToString(formatStringi) + " - " + dilim.Bitis.ToString(formatStringi) :
                    dilim.Baslangic.ToString(formatStringi);

        }
        public T[] ArrayKesiti<T>(this T[] veri, int indeks, int boyut)
        {
            T[] sonuc = new T[boyut];
            Array.Copy(veri, indeks, sonuc, 0, boyut);
            return sonuc;
        }

    }


}