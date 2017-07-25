# Fachw�rterbuch Studierendenverwaltung Uni Mainz (SpecialistDictionary)

Das Fachw�rterbuch(FWB) bietet eine Webseite zur Suche nach Fachw�rtern 
im Studierenden-Kontext (z.B. Zwangsexmatrikulation) und soll einheitliche �bersetzungen
der Fachw�rter in der Verwaltung erm�glichen.


## Datenmodell (SpecialistDic.Model)

Das Datenmodell stellt insbesondere POCOs bereit, um einzelne Eintr�ge des FWB zu repr�sentieren.


## Datenzugriff (SpecialistDic.DataAccess)

Im Datenzugriff sind Schnittstellen definiert, die den Zugriff auf die �bersetzungsdaten erm�glichen.
Zugriffsklassen sind nach CQRS-Prinzipien zu gestalten.


## Web-Frontend (SpecialistDic.Web)

Das WebFrontend bietet eine einfache Suchmaske zum Auffinden von �bersetzungseintr�gen an.