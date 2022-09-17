# Projektas Flight-Attendant

# Sistemos paskirtis

Projekto tikslas – supaprastinti lėktuvų bilietų paiešką, suteikiant informaciją apie skrydžius skirtinguose oro uostuose.

Veikimo principas – platformą sudaro dvi dalys: internetinė aplikacija, kuria naudosis skrydžių ieškantys asmenys, administratorius bei aplikacijų programavimo sąsaja (angl. trump. API).

Neregistruotas naudotojas gali peržiūrėti oro uostų sąrašą, norint peržvelgti avialinijas ir jų skrydžius skirtinguose oro uostuose, reikia prisiregistruoti. Pirma, registruotas naudotojas turi pasirinkti oro uostą, tada avialiniją. Atlikus šiuos du veiksmus tampa matomi atitinkami skrydžiai. Administratorius gali redaguoti oro uostų, avialinijų ir skrydžių sąrašus.

# Funkciniai reikalavimai

Neregistruotas sistemos naudotojas galės:

1.	Peržiūrėti reprezentacinį puslapį;
2.	Prisiregistruoti prie internetinės aplikacijos;
3.	Peržvelgti oro uostų sąrašą.

Registruotas sistemos naudotojas galės:

1.	Peržiūrėti reprezentacinį puslapį;
2.	Prisijungti prie platformos;
3.	Atsijungti;
4.	Peržiūrėti oro uostų sąrašą;
5.	Peržiūrėti oro uostuose esančių avialinijų sąrašą;
6.	Peržiūrėti avialinijų siūlomus skrydžius.

Administratorius galės:

1.	Pridėti, šalinti, redaguoti bei peržiūrėti oro uostus;
2.	Pridėti, šalinti, redaguoti bei peržiūrėti avialinijas;
3.	Pridėti, šalinti, redaguoti bei peržiūrėti skrydžius; 

# Sistemos architektūra

Sistemos sudedamosios dalys:

•	Kliento pusė (angl. Front-End) – naudojant React.js;
•	Serverio pusė (angl. Back-End) – naudojant .NET. Duomenų bazė – MySQL.

Sistemos talpinimui yra naudojamas Azure serveris. Kiekviena sistemos dalis yra diegiama tame pačiame serveryje. Internetinė aplikacija yra pasiekiama per HTTPS protokolą. Šios sistemos veikimui (pvz., duomenų manipuliavimui su duomenų baze) yra reikalingas Flight Attendant API. Jis vykdo duomenų mainus su duomenų baze - tam naudojama ORM sąsaja.


