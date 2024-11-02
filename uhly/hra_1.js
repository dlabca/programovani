function setup() {
  createCanvas(400, 400); // Set canvas size
  background(220); // Set background color
  rectMode(CORNER); // Set rectangle mode for easier positioning
}

function draw() {
      // Define prompt and result areas
  const promptAreaY = height - 50; // Bottom 50px for prompt
  const resultAreaY = height - 20; // Bottom 20px for result

  // Draw prompt area
  fill(200); // Light gray background
  rect(0, promptAreaY, width, 50); // Rectangle for prompt
  fill(0);
  textSize(16);
  text("Hádejte úhel (0-360):", 10, promptAreaY + 25); // Prompt text

  // User input for angle 1
  let angle1 = parseInt(prompt("Zadejte úhel první čáry (0-360):", 0)); // Prompt for angle 1

  // User input for angle 2
  let angle2 = parseInt(prompt("Zadejte úhel druhé čáry (0-360):", 0)); // Prompt for angle 2

  // Calculate angle between lines
  const angleBetween = abs(angle1 - angle2) % 360; // Calculate absolute angle difference

  // Draw line 1
  stroke(255, 0, 0); // Red color for line 1
  strokeWeight(2);
  line(width / 2, height / 2, width / 2 * cos(radians(angle1)), height / 2 * sin(radians(angle1)));

  // Draw line 2
  stroke(0, 0, 255); // Blue color for line 2
  strokeWeight(2);
  line(width / 2, height / 2, width / 2 * cos(radians(angle2)), height / 2 * sin(radians(angle2)));

  // Draw result area
  fill(200);
  rect(0, resultAreaY, width, 20); // Rectangle for result
  fill(0);
  text(`Úhel mezi čarami: ${angleBetween}°`, 10, resultAreaY + 15); // Display angle between lines
}
