# CatMash – Trouver le chat le plus mignon

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

