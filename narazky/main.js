// Pole s tématy
const topics = ["zvířata", "jídlo", "profese", "počasí", "oblečení", "lidé", "dopravní prostředky", "škola", "sport"];

// Pole s přívlastky
const adjectives = ["velký", "malý", "tlustý", "tenký", "rychlý", "pomalý", "chytrý", "hloupý", "krásný", "škaredý"];

// Pole s otázkami
const questions = ["Proč?", "Jak?", "Kde?", "Kdy?", "Kdo?", "Co?"];

// Pole s odpověďmi
const answers = [
  "Protože se mu to líbí.",
  "Aby se zahřál.",
  "Protože se nemůže podrbat na zátylku.",
  "Protože je zmatený.",
  "Protože je to jeho práce.",
  "Protože se nudí.",
  "Protože je to legrace.",
  "Protože je to jeho koníček.",
  "Protože je to jeho osud.",
  "Protože je to jeho životní cíl."
];

// Funkce pro generování náhodného vtipu
function generateRandomVtip() {
  // Náhodné téma
  const randomTopic = topics[Math.floor(Math.random() * topics.length)];

  // Náhodný přívlastek
  const randomAdjective = adjectives[Math.floor(Math.random() * adjectives.length)];

  // Náhodná otázka
  const randomQuestion = questions[Math.floor(Math.random() * questions.length)];

  // Náhodná odpověď
  const randomAnswer = answers[Math.floor(Math.random() * answers.length)];

  // Vytvoření vtipu
  const vtip = `${randomAdjective} ${randomTopic} ${randomQuestion} ${randomAnswer}`;

  // Vrací vtip
  return vtip;
}

// Vygenerování 10 náhodných vtipů
for (let i = 0; i < 10; i++) {
  console.log(generateRandomVtip());
}