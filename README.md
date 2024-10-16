# TwainScanApp
Prosta aplikacja skanująca dokument za pomocą sterownika TWAIN.

## Używane technologie
* C#
* .NET 4.8
* WinForms

## Jak używać aplikacji
1. Pobierz aplikację z sekcji release.
2. Kiedy uruchomisz aplikację, będzie wyglądała tak:
   
![TwinScanApp gotowe do działania](images/1.JPG "Gotowy do działania")

3. Wygląd aplikacji po zeskanowaniu przykładowego dokumentu:
   
![TwinScanApp po zeskanowaniu dokumentu](images/2.JPG "Wynik skanowania")
 
4. Możesz zapisać obraz w formacie .png lub .jpg. W przypadku zapisu w formacie .jpg do obrazka dodawane są przykładowe metadane EXIF.

![Dane EXIF po zapisaniu pliku](images/3.JPG "Dane EXIF")

Uwagi:
1. W systemie musi być zainstalowany sterownik TWAIN do skanera, lub urządzenia wielofunkcyjnego.
2. Program nie był testowany w systemie bez zainstalowanego sterownika - brak obsługi takiej sytuacji.
