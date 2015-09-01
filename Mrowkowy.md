# Introduction #

Dla algorytmu mrówkowego, czyli głównie dla Mikołaja, Bartka


# Parametry #

> private int _ants; //liczba mrowek_

> private int _maxAssigns; //liczba iteracji_

> private float _alpha; //współczynnik do feromonów (0,n)_

> private float _beta; //współczynnik do odległości (0,n)_

> private float _rho; //współczynnik parowania feromonów (0,1)_

> private float _q; //ilość pozostawianego feromonu (0,n)_

> private float _q0; //wartość feromonu powyżej której mrówki zachowują się zachłannie (0,n)_

> private float _t0; //początkowa wartość feromonu (0,n)_

> private float _Q; - licznik we współczynniku odległości (0,n)_


# Uwagi #

No na razie wiele to nie ma, ale idzie w dobrym kierunku bo coś już zaczęte. Przydałoby się, żeby w przeciągu tygodnia można to było doprowadzić do stanu możliwego do testowania.

Algorytm gotowy, niby działa, ale wyniki silnie zależą od parametrów wejściowych. nie wszystkie metody poimplementowane (np zwracanie kosztu po n iteracjach), ale tym ma się zająć Bartek. W każdym razie da się to już testować.