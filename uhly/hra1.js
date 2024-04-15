function setup() {
    createCanvas(400, 400);
    background(220);
    rectMode(CORNER);
  }
  
  function draw() {
    // Define prompt and result areas
    const promptAreaY = height - 50;
    const resultAreaY = height - 20;
  
    // Draw prompt area
    fill(200);
    rect(0, promptAreaY, width, 50);
    fill(0);
    textSize(16);
//    text(`Váš odhad: ${angle}°`, 10, promptAreaY + 25);
//    text(`Náhodný úhel: ${randomAngle}°`, 10, 25);  
    // Generate random angle
    let randomAngle = floor(random(0, 361));
  
    // Draw random angle
    fill(200);
    rect(0, 0, width, 50);
    fill(0);
    textSize(16);
    text(`Náhodný úhel: ${randomAngle}°`, 10, 25);
  
    // User input for angle
    let angle = parseInt(prompt("Zadejte úhel:", 0));
    text(`Váš odhad: ${angle}°`, 10, promptAreaY + 25);
  
    // Calculate absolute angle
    const absAngle = abs(angle) % 360;
  
    // Calculate difference between user input and random angle
    const diff = abs(randomAngle - absAngle);
  
    // Draw result area
    fill(200);
    rect(0, resultAreaY, width, 20);
    fill(0);
    text(`Rozdíl: ${diff}°`, 10, resultAreaY + 15);
  }