smallGridSize = 0.5;
gridSize = smallGridSize / 8;
boardOffset = 0.10;
dotSize = gridSize * 0.1;

difference() {
  translate([0, 0, smallGridSize/4])
    cube(size=[smallGridSize, smallGridSize, smallGridSize/2], center=true);
  translate([0, 0, boardOffset]) translate([0, 0, smallGridSize/2])
    cube(size=[smallGridSize*2, smallGridSize*2, smallGridSize], center=true);
  translate([-gridSize/2, -smallGridSize/2, 0]) rotate([0,0,-45]) translate([0,-smallGridSize,0])
    cube(size=[smallGridSize*2, smallGridSize*2, smallGridSize], center=true);
  translate([gridSize/2, -smallGridSize/2, 0]) rotate([0,0,45]) translate([0,-smallGridSize,0])
    cube(size=[smallGridSize*2, smallGridSize*2, smallGridSize], center=true);
}
for (y=[-3:3]) {
  wid = min(3+y, 3);
  for (x=[-wid:wid]) translate([x*gridSize, y*gridSize, boardOffset])
    cube([dotSize, dotSize, dotSize*2], center=true);
}