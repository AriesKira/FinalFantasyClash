# FINAL FANTASY CLASH

#### Constitution de l'équipe : Gil LINHARES

### Description du jeu
Jeu basé sur le gameplay de Clash Royale en 2D pour 2 joueurs en local.
Les joueurs peuvent composer leurs équipes de 4 personnages en sélectionnant parmi 6 personnages personnages différents (pour le moment).

Chaque personnage possède ses propres caractéristiques :
- Vie
- Attaque
- Vitesse
- Distance d'attaque
- Rayon de la zone d'aggro
- Coût
- Nombre de coups par attaque
- Type de personnage (MÊLÉE, DISTANCE)
- Type d'attaque (Cible unique, Zone/AOE)
- Rayon d'AOE
- Son d'attaque

La formule de dégâts est la suivante : attaque / nombre de coups.
Le personnage inflige donc au maximum des dégâts équivalents à sa statistique d'attaque.

Chaque personnage possède ses propres animations pour :
- Déplacement
- Idle (Attente)
- Attaque
- Mort
- Victoire

Pour les déplacements, chaque personnage utilise une IA de Pathfinding (A*) avec une liste de cibles, dont 3 par défaut (les objectifs adverses).

Lors de la partie, le joueur consomme du mana pour invoquer les personnages de son équipe.
Le joueur doit dépenser un montant de mana égal au coût de l'unité.
L'unité ne peut être invoquée que dans la zone jouable du joueur. Le joueur peut effectuer un "drag and drop" (glisser-déposer) pour invoquer les unités.

Le mana se remplit automatiquement à un rythme de 1/sec avec un maximum de 10.

Les tours ne se défendent pas en attaquant les personnages adverses. Je n'ai pas activé ni créé d'animations pour cela au départ afin de rendre le jeu plus dynamique. Après tests, je pense qu'intégrer l'attaque des tours pourrait être intéressant, mais je n'ai pas encore décidé de la méthode d'intégration qui permettrait de conserver le style 2D "rétro".

Lorsqu'une équipe détruit le château adverse, la partie prend fin. Les personnages de l'équipe perdante meurent et ceux de l'équipe gagnante exécutent leur animation de victoire.

Un écran de fin de partie s'affiche en fonction de l'équipe gagnante.
Depuis cet écran, les joueurs peuvent recommencer la partie avec les mêmes équipes ou lancer une nouvelle partie en retournant à l'écran de sélection.

### Avancement
Le jeu est 100% fonctionnel. L'équilibrage des personnages est à revoir.
De plus, une animation d'attaque spéciale (ultime) a été ajoutée à certains personnages, mais je n'ai pas encore décidé comment intégrer cette mécanique en jeu.

### POST-MORTEM / Évolutions futures
- Intégrer la fonctionnalité d'attaque spéciale pour les personnages.
- Refaire la carte avec des Tilemaps pour un rendu plus esthétique et plus propre.
- Ajouter plus de personnages.
- Ajouter un menu de paramètres de partie accessible depuis l'écran de sélection des personnages, permettant de modifier :
    - la vitesse de génération du mana.
    - les points de vie des tours et du château.
- Ajouter une limite de temps à la partie et désigner vainqueur le joueur ayant détruit le plus d'objectifs adverses.
- Proposer des statistiques de fin de partie (personnages les plus utilisés, dégâts infligés par unité, etc.).
- Passer à des decks d'équipe de 10 cartes avec une pioche aléatoire afin d'ajouter de la stratégie et du deckbuilding.
- Ajouter une attaque automatique aux tours (optionnel).
- Ajouter des décors plus poussés à la carte (assets déjà présents).
- Fix le problème des personnages qui se pousse sur les objectifs


### SOURCES (Assets utilisés et guides vidéo)

#### Fonctionnalités :
- IA PATHFINDING : https://youtu.be/jvtFUfJ6CP8?si=_ZDXa87QZXg-9WRo
- Drag and Drop : https://youtu.be/BGr-7GZJNXg?si=xPT5EXSwb63lXYfO
- Scroll view : https://youtu.be/Q-G-W93jhYc?si=vjDiyC0nHenZ18ZB
- Health bar : https://youtu.be/lYZayXViTN8?si=bi1f1hQnzxrkU5fo

#### Assets :
- Personnages : https://github.com/DaddyRaegen/ffbe_asset_dump/tree/main
- Décor : craftpix-net-106469-top-down-crystals-pixel-art
- Décor : craftpix-net-668008-free-bridges-top-down-pixel-art-asset-pack
- Décor : craftpix-net-381103-free-simple-summer-top-down-vector-tileset
- Décor : craftpix-net-695666-free-undead-tileset-top-down-pixel-art
- Décor : craftpix-net-934618-free-top-down-ruins-pixel-art
- Décor : craftpix-net-974061-free-rocks-and-stones-top-down-pixel-art
- Décor : kenney_tiny-battle
- Décor : kenney_tiny-town

Note : Tous les assets de décors n'ont pas été utilisés.