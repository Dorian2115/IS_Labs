Opis parametrów środowiska testowego - Laboratorium 1
Przedmiot: Integracja Systemów

1. System operacyjny: Windows 11
2. Środowisko programistyczne: Visual Studio 2026
3. Język programowania: C#
4. Wersja platformy: .NET 8.0
5. Typ aplikacji: Aplikacja konsolowa
6. Dodatkowe informacje: 
   - Aplikacja do poprawnego działania wymaga pliku "data.xml" znajdującego się w folderze "Assets". We właściwościach pliku ustawiono opcję "Copy always", więc plik powinien kopiować się automatycznie do folderu z plikiem wykonywalnym.

Podczas laboratorium pojawił się "problem" z liczbą leków o więcej niż jednej substancji czynnej (dla XMLReadWithDOMApproach i XMLReadWithSAXApproach było to 1027, a dla XMLReadWithXLSTDOM: 1026).
Wynikało to z różnych funkcji w instrukcji warunkowej sprawdzającej atrybuty. W dwóch pierwszych klasach była to: if (s != null && p != null), a w ostatniej: if (!string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(p)).
Metoda string.IsNullOrEmpty() odrzuca puste atrybuty dlatego w wyniku pojawił się o jeden mniej lek.
