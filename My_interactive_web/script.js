// Particles.js inicializace
        particlesJS('particles-js', {
            particles: {
                number: { value: 80 },
                color: { value: '#ffffff' },
                shape: { type: 'circle' },
                opacity: { value: 0.5 },
                size: { value: 3 },
                move: {
                    enable: true,
                    speed: 6
                }
            }
        });

        // Přepínání mezi sekcemi
        document.querySelectorAll('.nav-button').forEach(button => {
            button.addEventListener('click', function() {
                // Odstranění aktivní třídy ze všech tlačítek a sekcí
                document.querySelectorAll('.nav-button').forEach(btn => btn.classList.remove('active'));
                document.querySelectorAll('.section').forEach(section => section.classList.remove('active'));
                
                // Přidání aktivní třídy kliknutému tlačítku
                this.classList.add('active');
                
                // Zobrazení odpovídající sekce
                const sectionId = this.getAttribute('data-section');
                document.getElementById(sectionId).classList.add('active');
            });
        });

        // Dungeon Crawler hra
        document.getElementById('game1-card').addEventListener('click', function() {
            const gameContainer = document.getElementById('game1-container');
            gameContainer.style.display = gameContainer.style.display === 'none' || gameContainer.style.display === '' ? 'block' : 'none';
            
            if (gameContainer.style.display === 'block' && !gameInitialized) {
                initGame();
            }
        });

        let gameInitialized = false;
        let currentRoom;
        let player;
        let rooms = [];

        function initGame() {
            gameInitialized = true;
            
            // Inicializace výstupu hry
            const gameOutput = document.getElementById('gameOutput');
            gameOutput.innerHTML = 'Vitejte v dungeon crawler hre!\nZadejte prikaz (sever, jih, vychod, zapad, prozkoumej, boj, inventar, vezmi [nazev predmetu], pouzij [nazev predmetu], konec):\n\n';
            
            // Nastavení místností
            setupRooms();
            
            // Vytvoření hráče
            player = new Player("Hrac", 100);
            
            // Přidání ovládání hry
            const gameInput = document.getElementById('gameInput');
            const submitButton = document.getElementById('submitCommand');
            
            submitButton.addEventListener('click', function() {
                handleGameInput();
            });
            
            gameInput.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    handleGameInput();
                }
            });
        }
        
        function handleGameInput() {
            const gameInput = document.getElementById('gameInput');
            const command = gameInput.value.trim().toLowerCase();
            
            if (command === '') return;
            
            appendToGameOutput('> ' + command);
            
            if (command === 'konec') {
                appendToGameOutput('Konec hry.');
                return;
            }
            
            handleCommand(command);
            gameInput.value = '';
            gameInput.focus();
        }
        
        function appendToGameOutput(text) {
            const gameOutput = document.getElementById('gameOutput');
            gameOutput.innerHTML += text + '\n';
            gameOutput.scrollTop = gameOutput.scrollHeight;
        }
        
        function setupRooms() {
            const room1 = new Room("Vstupni hala", "Jste v temne vstupni hale. Je zde vychod na vychod.");
            const room2 = new Room("Chodba", "Jste v uzke chodbe. Jsou zde vychody na zapad a sever.");
            const room3 = new Room("Komnata", "Jste v rozlehle komnate. Je zde vychody na sever a jih.");
            const room4 = new Room("tajna mistnost komnaty", "jste v bytelne zkryte casti komnaty je zde poklad východy jsou na zapad a jih");
            const room5 = new Room("zbrojírna", "Jste ve zbrojírně , je tady několik brnění. Vychody:jih a vychod ");
            const room6 = new Room("bojová aréna", "Jste v bojové aréně ,je zde Boos. východy: sever");
            
            // Nastavení východů
            room1.setExits(null, null, room2, null);
            room2.setExits(room3, null, null, room1);
            room3.setExits(room4, room2, null, null);
            room4.setExits(null, room3, null, room5);
            room5.setExits(null, room6, room4, null);
            room6.setExits(room5, null, null, null);
            
            // Přidání nepřátel
            room2.addEnemy(new Enemy("Goblin", 30, 10));
            room3.addEnemy(new Enemy("Troll", 50, 20));
            room6.addEnemy(new Enemy("Boss", 150, 30));
            
            // Přidání předmětů
            room1.addItem(new Item("lektvar zivota", 100));
            room4.addItem(new Item("poklad", 0));
            room5.addItem(new Item("bronzove brneni", 10, 10));
            room5.addItem(new Item("platynove brneni", 15, 10));
            room5.addItem(new Item("diamantove brneni", 20, 10));
            
            // Aktuální místnost je úvodní
            currentRoom = room1;
            rooms = [room1, room2, room3, room4, room5, room6];
            
            appendToGameOutput("Presunuli jste se do: " + currentRoom.name);
            appendToGameOutput(currentRoom.description);
        }
        
        function handleCommand(command) {
            const parts = command.split(' ');
            const action = parts[0];
            const target = parts.length > 1 ? parts.slice(1).join(' ') : null;
            
            switch (action) {
                case "sever":
                    moveTo(currentRoom.north);
                    break;
                case "jih":
                    moveTo(currentRoom.south);
                    break;
                case "vychod":
                    moveTo(currentRoom.east);
                    break;
                case "zapad":
                    moveTo(currentRoom.west);
                    break;
                case "prozkoumej":
                    appendToGameOutput(currentRoom.description);
                    if (currentRoom.enemy) {
                        appendToGameOutput(`Zde je nepritel: ${currentRoom.enemy.name}`);
                    }
                    for (const item of currentRoom.items) {
                        appendToGameOutput(`Najdete: ${item.name}`);
                    }
                    break;
                case "boj":
                    if (currentRoom.enemy) {
                        combat(currentRoom.enemy);
                    } else {
                        appendToGameOutput("Zde neni zadny nepritel.");
                    }
                    break;
                case "inventar":
                    player.showInventory();
                    break;
                case "vezmi":
                    if (target) {
                        takeItem(target);
                    } else {
                        appendToGameOutput("Nezadal jste nazev predmetu k sebrani.");
                    }
                    break;
                case "pouzij":
                    if (target) {
                        useItem(target);
                    } else {
                        appendToGameOutput("Nezadal jste nazev predmetu k pouziti.");
                    }
                    break;
                case "clear":
                    document.getElementById('gameOutput').innerHTML = '';
                    break;
                case "help":
                    appendToGameOutput("Prikazy: sever, jih, vychod, zapad, prozkoumej, boj, inventar, vezmi [nazev predmetu], pouzij [nazev predmetu], clear, konec");
                    break;
                default:
                    appendToGameOutput("Neznamy prikaz.");
                    break;
            }
        }
        
        function moveTo(room) {
            if (room) {
                currentRoom = room;
                appendToGameOutput("Presunuli jste se do: " + currentRoom.name);
                appendToGameOutput(currentRoom.description);
                if (currentRoom.enemy) {
                    appendToGameOutput(`Zde je nepritel: ${currentRoom.enemy.name}`);
                }
            } else {
                appendToGameOutput("Timto smerem nemuzete jit.");
            }
        }
        
        function combat(enemy) {
            appendToGameOutput(`Bojujete s ${enemy.name}!`);
            
            let inCombat = true;
            
            const combatInterval = setInterval(() => {
                if (!inCombat || enemy.health <= 0 || player.health <= 0) {
                    clearInterval(combatInterval);
                    
                    if (player.health <= 0) {
                        appendToGameOutput("Byli jste porazeni! Konec hry.");
                        // Reset hry
                        initGame();
                    } else if (enemy.health <= 0) {
                        appendToGameOutput(`Porazili jste ${enemy.name}!`);
                        currentRoom.removeEnemy();
                    }
                    return;
                }
                
                appendToGameOutput(`Hrac zdravi: ${player.health}, ${enemy.name} zdravi: ${enemy.health}`);
                appendToGameOutput("Zadejte prikaz (utok, utek):");
                
                const gameInput = document.getElementById('gameInput');
                const submitButton = document.getElementById('submitCommand');
                
                const originalSubmitAction = submitButton.onclick;
                
                submitButton.onclick = () => {
                    const combatCommand = gameInput.value.trim().toLowerCase();
                    appendToGameOutput('> ' + combatCommand);
                    
                    if (combatCommand === 'utok') {
                        player.attack(enemy);
                        if (enemy.health > 0) {
                            enemy.attack(player);
                        } else {
                            inCombat = false;
                        }
                    } else if (combatCommand === 'utek') {
                        appendToGameOutput("Utekli jste z boje!");
                        inCombat = false;
                    } else {
                        appendToGameOutput("Neznamy prikaz.");
                    }
                    
                    gameInput.value = '';
                    
                    if (!inCombat) {
                        submitButton.onclick = originalSubmitAction;
                    }
                };
                
                gameInput.onkeypress = (e) => {
                    if (e.key === 'Enter') {
                        submitButton.onclick();
                    }
                };
                
                // Pozastavení intervalu, dokud uživatel nezadá příkaz
                clearInterval(combatInterval);
            }, 1000);
        }
        
        function takeItem(itemName) {
            const itemIndex = currentRoom.items.findIndex(i => i.name.toLowerCase() === itemName.toLowerCase());
            
            if (itemIndex !== -1) {
                const item = currentRoom.items[itemIndex];
                player.addItem(item);
                currentRoom.items.splice(itemIndex, 1);
                appendToGameOutput(`Sebrali jste ${itemName}.`);
            } else {
                appendToGameOutput(`Predmet ${itemName} zde neni.`);
            }
        }
        
        function useItem(itemName) {
            const item = player.findItem(itemName.trim());
            
            if (item) {
                const parts = item.name.split(' ');
                if (item.name.toLowerCase() === "lektvar zivota") {
                    player.increaseHealth(item.value);
                    player.removeItem(item);
                    appendToGameOutput(`Pouzili jste ${itemName} a ziskali ${item.value} zdravi.`);
                } else if (item.name.toLowerCase() === "poklad") {
                    player.removeItem(item);
                    player.addItem(new Item("mec bohu", 30, 1));
                    appendToGameOutput(`Pouzili jste poklad a ziskali jste mec bohu. mec bohu je mec ktery dava 30 pozkozeni`);
                } else if (parts.length > 1 && parts[1].toLowerCase() === "brneni") {
                    const armorValue = item.value;
                    player.removeItem(item);
                    player.brneniValue = item.value;
                    player.brneniDurabiliti = item.value2;
                    appendToGameOutput(`Pouzili jste ${itemName} a ziskali ${armorValue} brneni.`);
                } else if (parts.length > 1 && parts[0].toLowerCase() === "mec") {
                    const damageValue = item.value;
                    player.removeItem(item);
                    player.mecdurabiliti = item.value2;
                    player.damagevalue = item.value;
                    appendToGameOutput(`Pouzili jste ${itemName} a ziskali ${damageValue} silny utok.`);
                } else {
                    appendToGameOutput(`Nemuzete pouzit ${itemName}.`);
                }
            } else {
                appendToGameOutput(`Nemate ${itemName} ve svem inventari.`);
            }
        }
        
        // Třída pro místnosti
        class Room {
            constructor(name, description) {
                this.name = name;
                this.description = description;
                this.north = null;
                this.south = null;
                this.east = null;
                this.west = null;
                this.enemy = null;
                this.items = [];
            }
            
            setExits(north, south, east, west) {
                this.north = north;
                this.south = south;
                this.east = east;
                this.west = west;
            }
            
            isRoomExist(room) {
                return room !== null;
            }
            
            addEnemy(enemy) {
                this.enemy = enemy;
            }
            
            removeEnemy() {
                this.enemy = null;
            }
            
            addItem(item) {
                this.items.push(item);
            }
        }
        
        // Třída pro hráče
        class Player {
            constructor(name, health) {
                this.name = name;
                this.health = health;
                this.inventory = [];
                this.brneniValue = 0;
                this.brneniDurabiliti = 0;
                this.mecdurabiliti = 0;
                this.damagevalue = 0;
                this.baseAttackPower = 10;
            }
            
            strongDamage() {
                if (this.mecdurabiliti <= 0) {
                    this.damagevalue = this.baseAttackPower;
                    appendToGameOutput("mec se vam rozbil");
                }
                this.mecdurabiliti--;
                return this.damagevalue > 0 ? this.damagevalue : this.baseAttackPower;
            }
            
            attack(enemy) {
                appendToGameOutput(`Utocite na ${enemy.name}!`);
                enemy.takeDamage(this.strongDamage());
            }
            
            takeDamage(damage) {
                damage -= this.brneniValue;
                damage = damage < 0 ? 0 : damage;
                this.brneniDurabiliti -= 1;
                this.health -= damage;
                appendToGameOutput(`Obdrzeli jste ${damage} poskozeni.`);
                if (this.brneniDurabiliti <= 0) {
                    this.brneniValue = 0;
                    appendToGameOutput("brnění se vám rozbilo");
                }
            }
            
            showInventory() {
                appendToGameOutput("Vas inventar:");
                if (this.inventory.length === 0) {
                    appendToGameOutput("Váš inventář je prázdný.");
                } else {
                    for (const item of this.inventory) {
                        appendToGameOutput(item.name);
                    }
                }
            }
            
            addItem(item) {
                this.inventory.push(item);
                appendToGameOutput(`Pridali jste do inventare: ${item.name}`);
            }
            
            findItem(itemName) {
                return this.inventory.find(i => i.name.toLowerCase() === itemName.toLowerCase());
            }
            
            removeItem(item) {
                const index = this.inventory.indexOf(item);
                if (index !== -1) {
                    this.inventory.splice(index, 1);
                }
            }
            
            increaseHealth(amount) {
                this.health += amount;
                appendToGameOutput(`Zdravi zvyseno o ${amount}. Aktualni zdravi: ${this.health}`);
            }
        }
        
        // Třída pro nepřátele
        class Enemy {
            constructor(name, health, attackPower) {
                this.name = name;
                this.health = health;
                this.attackPower = attackPower;
            }
            
            attack(player) {
                appendToGameOutput(`${this.name} utoci na vas!`);
                player.takeDamage(this.attackPower);
            }
            
            takeDamage(damage) {
                this.health -= damage;
                appendToGameOutput(`${this.name} obdrzel ${damage} poskozeni.`);
            }
        }
        
        // Třída pro předměty
        class Item {
            constructor(name, value, value2 = 0) {
                this.name = name;
                this.value = value;
                this.value2 = value2;
            }
        }