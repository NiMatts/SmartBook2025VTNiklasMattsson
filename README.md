# SmartBook2025VTNiklasMattsson
Enklare biblotekts program där funktionalliteten kan beskrivas som:
- Lägg till en bok (titel, författare, ISBN, kategori)
- Ta bort en bok (via titel eller ISBN)
- Lista alla böcker (sorterade t.ex. efter titel) med LINQ
- Sök efter bok (titel eller författare) med LINQ
- Markera bok som "utlånad" eller "tillgänglig"
- Spara och läsa in biblioteket från fil (JSON)

Visa begränsningar kan förekomma i interaktion med ISBN då den är lite slarvigt genererad och inte manuellt inskriven.
Idagsläget ser jag inga problem med det men om begränsningen på dubbleter skulle lyftas så skulle ISBN kunna börja ge oönskvärt förenklade resultat.
Programmets funktionallitet navigeras med siffror alternativt avslustas med Q.
Jag har testat att visa delar av skapandet av nya böcker fungerar som jag vill.
