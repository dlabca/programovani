let randomAngle1;
let randomAngle2;

function setup() {
  createCanvas(400, 500);
  background(220);
  rectMode(CORNER);
  randomAngle1 = floor(random(0, 361));
  randomAngle2 = floor(random(0, 361));
  kresleni();
}

function draw() {
  // Funkce draw není potřeba, protože kreslíme pouze jednou v setup
}

function kresleni() {
  let randomRadius = 200;

  stroke(255, 0, 0);
  line(200, 200, 200 + randomRadius * cos(radians(randomAngle1)), 200 + randomRadius * sin(radians(randomAngle1)));

  stroke(0, 0, 0);
  line(200, 200, 200 + randomRadius * cos(radians(randomAngle2)), 200 + randomRadius * sin(radians(randomAngle2)));

  stroke(0, 0, 0)
  fill(200);
  rect(0, height - 100, width, 100);

  // Po nakreslení obou čar vypočítáme úhel
  let angle = abs(randomAngle1 - randomAngle2);
  console.log("Úhel mezi čarami je: " + angle);
}
