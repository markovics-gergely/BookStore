# BookStore
 BookStore ASP.NET Web API
 Szoftverfejlesztés .NET platformra nevű tárgy keretében lérejött projekt
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
