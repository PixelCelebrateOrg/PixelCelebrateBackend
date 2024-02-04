Proiect realizat in .NET 6.0.
Contine HTTP requests pentru operatii CRUD pe tabela User.
Actiunile posibile sunt login, atat ca admin cat si ca angajat, adminul poate adauga un user, sa ii dea update sau delete. Acesta poate si sa seteze cu cate zile inainte de ziua de nastere a unui user sa se trimita un email celorlalti useri.
Un angajat poate vedea ceilalti useri, si datele lui personale.
Angajatii pot vedea doar numele si emailul utilizatorilor, adminii pot vedea toate datele.

Baza de date a fost realizata in Microsoft SQL Server.
Pentru email, am folosit mailtrap pentru a prinde toate emailurile trimise de aplicatie. Pentru mailtrap, e nevoie de un cont, din care sunt folosite credentiale pentru conectare, care trebuie introduse in appsetting.json.