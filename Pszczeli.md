# Introduction #

O algorytmie pszczelim, głównie dla Kuby, Marcina, Piotra P.


# Details #

Zapoznajcie się z tym algorytmem i przede wszystkim przedstawcie jakie można użyć parametry do modelowania rozwiązania.

Przydałoby się przyjąć jakiś stały interfejs, żeby dorobienie do tego GUI było prostsze.


## Piotr Popowicz mówi: :D ##

Żeby nie było, że nic nie robię.

Dla ludzi, którzy chcą coś wiedzieć o algorytmie pszczelim:
http://www.fi.muni.cz/usr/popelinsky/lectures/kdd/Bees%20algorithm.ppt

Bardzo wartościowa prezentacja, dobrze tłumacząca zasadę algorytmu.

Dla potomności: (ogólnej wiedzy) (projektantów GUI)

Parametry jakie przyjmuje algorytm:
  1. Liczba pszczół zwiadowców (**n** > 1)
  1. Liczba wybranych miejsc (**m** ε <1, n>)
  1. Liczba lepszych miejsc (**e** ε <1, m>)
  1. Liczba pszczół wysyłanych do lepszych miejsc (int **nep**)
  1. Liczba pszczół wysłanych do pozostałych wybranych miejsc (int **nsp**)
  1. Rozmiar sąsiedztwa (% całego rozmiaru problemu) (**ngh** ε <0, 1>)
  1. Maksymalna liczba iteracji (**imax**)

Nazwy (takie najczęściej występują) parametrów podałem boldem w nawiasach wraz z przedziałami ich wartości.


Tyle na dzisiaj. Dziękuję za uwagę. Do zobaczenia wkrótce.

# Uwagi #

Dobra, to ten algorytm wygląda obiecująco. Niech reszta z grupy też coś zrobi i zobaczy czy można to wykorzystać. Daj też linka skąd to wziąłeś.
Zacznijcie też od zaimplementowania interfejsu.

link: http://msdn.microsoft.com/en-us/magazine/gg983491.aspx

# Uwagi 2 #

Trudno było coś zrobić z tą wersją z msdn, dlatego napisałem własną implementację na podstawie algorytmu przedstawionego w prezentacji (na górze strony).
Pewnie nie jest doskonały, ale mniej więcej działa.
Można go potestować na wpisanych danych.
Jak już będzie gotowy interfejs to go dopiszę i postaram się o jakieś komentarze.
Jak się komuś chce (i wie jak) to może przerobić ten z msdn. Mielibyśmy dwie różne wersje pszczół (tamto jest jakieś ABC czy SBC czy jak to tam zwał, a moje zwykłe BA)

Teraz można zacząć zgłaszać swoje uwagi, jak się komuś nie podoba to mówić.

# Uwagi 3 #

Wcale nie było tak trudno, pozmieniałem to z msdn i nawet dobrze to wygląda:) Jest teraz w branchu "Pszczoly". Poprzedni branch ("pszczeli") można usunąć.

Btw. Bee Algorithm to jest to samo co Simulated Bee Colony Algorithm

Parametry jakie przyjmuje moja implementacja algorytmu:
  * int totalNumberBees - max liczba pszczół
  * int numberScout - liczba pszczół-zwiadowców (numberScout < totalNumberBees)
  * int maxNumberVisits - jeśli pszczoła po maxNumberVisits wizytach w tym samym miejscu nie znajdzie lepszego sąsiedztwa to jej status zostaje zmieniony na nieaktywny
  * int maxNumberCycles - liczba iteracji algorytmu
  * double probPersuasion - prawdopodobieństwo że nieaktywna pszczoła zostanie przekonana tańcem innej pszczoły do lotu (oprócz tego żeby pszczoła poleciała, miejsce proponowane przez tańczącą pszczołę musi być lepsze niż to w którym była ostatnio) (0.0 <= probPersuasion <= 1.0)
  * double probMistake - prawdopodobieństwo że pszczoła się pomyli, tzn. nie poleci do lepszego sąsiedztwa lub poleci do gorszego sąsiedztwa (0.0 <= probMistake <= 1.0)

Pojedyncze rozwiązanie przechowuję jako tablicę intów, gdzie indeksy są lokacjami a wartości pól - fabrykami.

Nie testowałem jeszcze tego algorytmu ale jak znajdę trochę czasu to to zrobię. (+ trzeba zaimplementować resztę metod z interfejsu, dobrze by było jakby koordynator na wiki opisał o co dokładnie w nich chodzi - np. co ma zwracać GetCost?)