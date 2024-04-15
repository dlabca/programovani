function setup() {
  createCanvas(400, 500);
  background(220);
  rectMode(CORNER);
}
function draw() {
  let angle = 0;
  let randomAngle = floor(random(0, 361));
  let randomAngle2 = floor(random(0, 361));
  //  stroke(255, 0, 0); // Red color for line 1
  //  strokeWeight(2);
  line(width / 2, height / 2, width / 2 * cos(radians(randomAngle)), height / 2 * sin(radians(randomAngle)));
  line(width / 2, height / 2, width / 2 * cos(radians(randomAngle2)), height / 2 * sin(radians(randomAngle2)));
  //  nostroke()
  for (angle = 0; angle <= 0;) {
    angle = parseInt(prompt("kolik je váš odhad velikosti úhlu:", 0));
  }
  fill(200);
  rect(0, height - 100, width, 100);
  fill(0);
  textSize(15);
  text(`Úhel mezi čarami: ${randomAngle2}°`, 10, 425); // Display angle between lines
  text(`Váš odhad: ${angle}°`, 10, 450);
  let angleBetween = abs(randomAngle2 - angle) % 360;
  if (angleBetween > 180) {
    angleBetween = 360 - angleBetween;
  }
  text(`Rozdíl mezi Vaším odhadem a reálným úhlem: ${angleBetween}°`, 10, 475);

}