# BookStore
 BookStore ASP.NET Web API
 Szoftverfejlesztés .NET platformra nevű tárgy keretében lérejött projekt
 
# Könyvtár Kölcsönző

## Feladat [2-3 mondat]

Egy olyan alkalmazás készítése, amely képes egy könytár adatait eltárolni. 
Lehet benne könyvtári tagokat, kölcsönzéseket és a könyvek adatbázisát kezelni. 
Meg lehet adni a könyvek szerzőinek is alapvető adatait (születési év, hely, stb). 
Lehet új tagokat, könyveket létrehozni, törölni vagy módosítani, és köztük kölcsönzéseket párosítani.

## A kisháziban elérhető funkciók [adatmódosítással járó is legyen benne]
### Módosító
- Új tag, könyv, szerző hozzáadása
- Tagok vagy könyvek törlése
- Kölcsönzés létrehozása, visszavitelkor időpont rögzítése
- 4 entitás adatainak szerkesztése (pl. könyv címének megváltoztatása) 
### Lekérdező
- Tag, könyv vagy szerző adatainak lekérdezése
- Aktuális kölcsönzések kilistázása
- Megmondani egy könyvről kinél van
- Tag kölcsönzött könyvei kilistázása
- Szerző könyveinek kilistázása

## Adatbázis entitások [min. 3 db.]
- Tag (Kölcsönző)
- Könyv
- Kölcsönzés
- Szerző
 
## Megvalósított funkciók
- Szerver oldali autentikáció. Saját token provider készítése, Azure AD B2C-re (ingyenes szint) építve
- szerver oldali hozzáférés-szabályozás, claim alapú hozzáférés-szabályozás
- OpenAPI leíró (swagger) alapú dokumentáció
- automatikus újrapróbálkozás beállítása tranziens adatbázishibák (pl. connection timeout) ellen
- alternatív kulcs bevezetése valamelyik entitásban
- adatbetöltés (seeding) migráció segítségével
- értékkonverter (value converter) alkalmazása EF Core leképezésben, saját value converter 5
- unit tesztek készítése
   * a unit tesztekben a mock objektumok injektálása
   * sqlite (vagy in-memory sqlite) használata teszteléshez
- külső komponens használata DTO-k inicializálására, AutoMapper

## Képek

![Swagger](https://github.com/markovics-gergely/BookStore/blob/main/pics/swagger.PNG)
![Login](https://github.com/markovics-gergely/BookStore/blob/main/pics/login.PNG)
