Kompilasi
---------
Program C# dicompile menggunakan visual studio 2013. Buka file .sln dari tiap-tiap komponen
yang ingin dicompile, kemudian Build solution.

Instalasi
---------
Prasyarat sebelum memakai program:
1. Memiliki Database SQL yang bernama db_crawler.
2. Membuat struktur database db_crawler sehingga memiliki stuktur seperti db_crawler.sql atau
   Langsung mengimport file db_crawler.sql ke database anda.
3. Menginstall HTML server dan PHP server.
4. Meletakkan isi folder bin/ ke dalam folder HTML server anda.

Penggunaan
----------
1. Pakailah index.html sebagai antarmuka pertama ketika melakukan search (seperti membuka google.com
   sebelum melakukan searching).
2. index.html juga dapat digunakan sebagai antarmuka (GUI) untuk melakukan crawling terhadap website.
3. Hasil search ditampilkan seperti website searching secara umum, sehingga mudah dipakai.