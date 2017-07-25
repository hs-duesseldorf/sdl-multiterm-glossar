# Fachwörterbuch Studierendenverwaltung Uni Mainz (SpecialistDictionary)

Das Fachwörterbuch(FWB) bietet eine Webseite zur Suche nach Fachwörtern 
im Studierenden-Kontext (z.B. Zwangsexmatrikulation) und soll einheitliche Übersetzungen
der Fachwörter in der Verwaltung ermöglichen.


## Datenmodell (SpecialistDic.Model)

Das Datenmodell stellt insbesondere POCOs bereit, um einzelne Einträge des FWB zu repräsentieren.


## Datenzugriff (SpecialistDic.DataAccess)

Im Datenzugriff sind Schnittstellen definiert, die den Zugriff auf die Übersetzungsdaten ermöglichen.
Zugriffsklassen sind nach CQRS-Prinzipien zu gestalten.


## Web-Frontend (SpecialistDic.Web)

Das WebFrontend bietet eine einfache Suchmaske zum Auffinden von Übersetzungseinträgen an.