# CatMash – Trouver le chat le plus mignon

## 🔍 Feedback du test technique (Negative feedback)

### ⚙️ Backend

- Il y a une race condition dans le CatService  
- Si on refresh sur /voting on a un 404  
- Connaît-il async/await. LoadCats().GetAwaiter().GetResult(); c’est une mauvaise pratique  
- Des try/catch qui font juste des throws …  
- Il a fait une méthode juste pour faire un _totalVotes++; (qui n’est pas atomic btw). Alors que le calcul du ELO se fait au milieu de la méthode de vote. Il faudrait decouper ce passage pour avoir un test unitaire ELO  
- Le fichier CatMash.Api.http n’est pas mis à jour (à supprimer ou update)  
- C’est bien beau de faire une CorsPolicy, mais dommage de de mettre AllowCredentials  
- Dans le /cats/random (qui n’est pas une norme REST), le .Distinct (qui ne sert a rien sans IEquatable) est fait après le Take(2). Donc on en prend deux et si c’est le même on en garde qu’un ?  
- Créer un constructeur par défaut parce qu'on en a besoin pour les tests unitaire prouve qu’il ne connaît pas les mocks  

---

### Frontend

#### Les plus
- Application en temps réel  
- Code plutôt propre, facile à lire et bien structuré  
- Bons patterns Angular (écoute temps réel -> BehaviorSubject)  
- Front-end short & concise, et assez astucieux  

#### Les moins
- Quelques oublis de typages, quelques petits passages ou l'on manque de rigueur, où l'on ne gère pas les potentielles erreurs  
- Quelques légers anti-patterns : Observables nestés, init constructor vs ngOnInit  
- Manque de responsivness  
---

## 🌐 Accès à l'application

👉 Tu peux tester l'application ici : [CatMash pour L'Atelier](https://catmash-frontend-wlmt.onrender.com)

---

## 🏆 Logique des gagnants

Pour garantir un classement juste et éviter toute ambiguïté :

- L’application utilise un **algorithme de scoring de type Elo**
- Seuls les **3 meilleurs chats** sont considérés pour le podium
- En cas d’égalité :
  - Les chats partagent le même rang (**classement dense**)
- Selon les votes :
  - Il peut y avoir **0, 1, 2 ou 3 gagnants**

> 💡 Ce comportement est un choix de conception visant à garder un classement clair et équitable.

---

## ⚙️ Fonctionnalités

1. Comparez et votez entre deux chats aléatoires 
2. Suivez un classement dynamique évoluant en temps réel
3. Découvrez le podium des 3 chats mignons
4. Synchronisation instantanée grâce à SignalR  
5. Gestion des égalités dans le classement  

---

## 🧪 Technologies utilisées

### Backend
- .NET 9 (Minimal API)
- SignalR
- HttpClient (récupération des données externes)

### Frontend
- Angular v19
- RxJS
- SCSS

---

## 🚀 Lancer le projet

### Backend
```bash
dotnet run
```
### Frontend
```bash
npm install
ng serve
```
---
## 📌 Auteur

Projet réalisé dans le cadre d’un test technique.

