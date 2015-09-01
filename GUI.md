# Introduction #

GUI - zasadniczo dotyczy Szymona, Maćka, Witka.


# Details #

Gui powinno umożliwiać przede wszystkim :

  * wybranie rozwiązania problemu (jakieś taby np)
  * załadowanie testu (obczajcie obsługę plików)
  * dla każdego algorytmu rozmaite dane do wyboru gotowe jak i możliwość wprowadzenia ręcznie
  * przycisk startu

Szczegóły techniczne:
  * do tworzenia wykresów wykorzystywać będziemy Microsoft Chart Controls

Najlepiej zacznijcie jakoś modelować ten interfejs, zróbcie jakiś projekt.
Jeśli macie wiecej pomysłów to dopiszcie.

Tu dajcie wszystkie linki dotyczące GUI.


wideo instruktaż w jaki sposób stworzyć wykres:
http://www.youtube.com/watch?v=bRnUAwUFIgY

MS Chart samples:
http://archive.msdn.microsoft.com/mschart/Release/ProjectReleases.aspx?ReleaseId=4418

Przy czym ostrzegam że zarówno film na youtube jak i sample nie są poprawne :/ Działanie zgodnie z nimi nie daje poprawnego wykresu, mówiąc delikatnie microsoft ssie.


# Uwagi #

  * Wczytywanie danych raczej dobrze nie działa jeszcze, bo dość mocno się zwiesza przy tym. Może trzeba odpalić to w jakimś innym wątku. Poza tym przydałoby się abyście dodali już wszystkie parametry i niech ktoś się zajmie wykresami i zapisywaniem danych - to w osobnym okienku. W praniu zobaczymy jak to pójdzie.

  * Można dodać jakieś wodotryski, ale to potem, bo na razie interfejs graficznie wygląda ubogo, a mam wrażanie, że pani Kwiecień zwraca na to uwagę.

**WAŻNE - zdecydujcie jak chcecie przekazywać parametry do algorytmów, czyli jak będzie wam najwygodniej. Czy przez konstruktor i czy przez ten słownik i z rzutowaniem?**

# Odpowiedź na uwagi #

> W ktorym momencie wczytywanie sie zwiesza? u mnie nie widze jakichs specjalnych problemow poki co, przetestuje to. Postaram sie ogarnac wykresy

> Zgadzam sie że interfejs wygląda ubogo, jednakże mam prośbe - jeśli ktoś ma jakąś wizje w jaki sposób zmienić/polepszyć wygląd gui to niech napisze, narysuje w paincie, stworzy montaż słowno-muzyczny owa wizje opisujacy. Ja wiem że prosto stwierdzić 'brzydkie' jednakże żaden z nas nie jest artystą, dlatego z chęcią przyjmiemy wszelkie opinie i uwagi od bardziej uzdolnionych, a następnie wykonamy.

> Co do przekazywania parametrow - jeszcze nie wiem co bedzie lepsze, konstruktor wydaje sie byc hmm, nieco mniej elegancki i wyrafinowany, ale chyba prostszy. Wybieramy konstruktor.