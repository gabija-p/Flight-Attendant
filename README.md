# Projektas Flight-Attendant

# Sistemos paskirtis

Projekto tikslas – supaprastinti lėktuvų bilietų paiešką, suteikiant informaciją apie skrydžius skirtinguose oro uostuose.

Veikimo principas – platformą sudaro dvi dalys: internetinė aplikacija, kuria naudosis skrydžių ieškantys asmenys, administratorius bei aplikacijų programavimo sąsaja (angl. trump. API).

Neregistruotas naudotojas gali peržiūrėti oro uostų sąrašą, norint peržvelgti avialinijas ir jų skrydžius skirtinguose oro uostuose, reikia prisiregistruoti. Pirma, registruotas naudotojas turi pasirinkti oro uostą, tada avialiniją. Atlikus šiuos du veiksmus tampa matomi atitinkami skrydžiai. Administratorius gali redaguoti oro uostų, avialinijų ir skrydžių sąrašus.

# Sistemos architektūra

Sistemos sudedamosios dalys:

•	Kliento pusė (angl. Front-End) – naudojant React.js;
•	Serverio pusė (angl. Back-End) – naudojant .NET. Duomenų bazė – MySQL.

Sistemos talpinimui yra naudojamas Azure serveris. Kiekviena sistemos dalis yra diegiama tame pačiame serveryje. Internetinė aplikacija yra pasiekiama per HTTPS protokolą. Šios sistemos veikimui (pvz., duomenų manipuliavimui su duomenų baze) yra reikalingas Flight Attendant API. Jis vykdo duomenų mainus su duomenų baze - tam naudojama ORM sąsaja.

# API specifikacija

  # Registracija ir prisijungimas:
    
    POST {{server_url}}/login
    POST {{server_url}}/register
    
  # Oro uostai:
    
    GET {{server_url}}/airports
    GET {{server_url}}/airports/:id
    POST {{server_url}}/airports
    PUT {{server_url}}/airports/:id
    DELETE {{server_url}}/airports/:id
  
  # Oro linijos:
    
    GET {{server_url}}/airports/:id/airlines
    GET {{server_url}}/airports/:id/airlines/:airline_id
    POST {{server_url}}/airports/:id/airlines
    PUT {{server_url}}/airports/:id/airlines/:airline_id
    DELETE {{server_url}}/airports/:id/airlines/:airline_id
    
  # Skrydžiai:
  
    GET {{server_url}}/airports/:id/airlines/flights
    GET {{server_url}}/airports/:id/airlines/:airline_id/flights/:flight_id
    POST {{server_url}}/airports/:id/airlines/flights
    PUT {{server_url}}/airports/:id/airlines/:airline_id/flights/:flight_id
    DELETE {{server_url}}/airports/:id/airlines/:airline_id/:flight_id
    
  # Naudotojo sąsajos projektas: https://miro.com/app/board/uXjVP3OcHxs=/?share_link_id=720284269182
    
