let randomAngle1;
let randomAngle2;
let odpoved = 0;
let angleBetween = 0;
let angle;
let pokracovat = false;
let uspechy = 0;
let neuspechy = 0;
let ignorovanyrozdil = 0;
let pocether = 0;
let procentauspesnosti = 0;
let randomRadius = 150; // Zmenšeno pro lepší vzhled na canvasu

function setup() {
    let canvas = createCanvas(400, 400);
    canvas.parent("canvas-container"); // Umístí canvas do divu v HTML
    kresleni();
}

function draw() {
    if (odpoved > 0) {
        pocether++;
        background(220);
        vykresliLinie();
        zobrazStatistiky();
        zobrazOdpoved();
        odpoved = 0;
    }
    if (pokracovat) {
        pokracovat = false;
        background(220);
        kresleni();
        zobrazStatistiky();
    }
}

// Funkce pro vykreslení čar a výpočet úhlu
function kresleni() {
    randomAngle1 = floor(random(0, 361));
    randomAngle2 = floor(random(0, 361));
    
    stroke(255, 0, 0);
    line(width / 2, height / 2, width / 2 + randomRadius * cos(radians(randomAngle1)), height / 2 + randomRadius * sin(radians(randomAngle1)));
    
    stroke(0, 0, 0);
    line(width / 2, height / 2, width / 2 + randomRadius * cos(radians(randomAngle2)), height / 2 + randomRadius * sin(radians(randomAngle2)));
    
    angle = abs(randomAngle1 - randomAngle2);
    if (angle > 180) angle = 360 - angle;
}

// Zobrazení výsledků a statistik
function zobrazStatistiky() {
    let successRate = document.getElementById("success-rate");
    let successCount = document.getElementById("success-count");
    let failureCount = document.getElementById("failure-count");

    procentauspesnosti = pocether > 0 ? floor((uspechy / pocether) * 100) : 0;
    successRate.textContent = `Úspěšnost: ${procentauspesnosti}%`;
    successCount.textContent = `Úspěchy: ${uspechy}`;
    failureCount.textContent = `Neúspěchy: ${neuspechy}`;
}

function zobrazOdpoved() {
    angleBetween = abs(angle - odpoved);
    if (angleBetween <= ignorovanyrozdil) {
        uspechy++;
    } else {
        neuspechy++;
    }
}

// Funkce pro získání hodnoty z formuláře
function storeAnswer() {
    odpoved = parseInt(document.getElementById("answerInput").value);
    ignorovanyrozdil = parseInt(document.getElementById("ignorovanyrozdil").value);
}

function Pokracovat() {
    pokracovat = true;
}
function startGame() {
    const settings = document.getElementById("settings");
    const game = document.getElementById("game");
    const settingsIcon = document.querySelector(".settings-icon");

    // Skryje nastavení s animací a zobrazí hru
    settings.classList.add("hidden");
    setTimeout(() => {
        settings.style.display = "none";
        game.style.display = "block";
        settingsIcon.style.display = "block";
    }, 500); // Počká na dokončení animace
}

function toggleSettings() {
    const settings = document.getElementById("settings");
    const game = document.getElementById("game");

    if (settings.style.display === "none" || settings.classList.contains("hidden")) {
        settings.classList.remove("hidden");
        settings.style.display = "block";
        game.style.display = "none";
    } else {
        settings.classList.add("hidden");
        setTimeout(() => {
            settings.style.display = "none";
            game.style.display = "block";
        }, 500);
    }
}
