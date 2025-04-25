smallGridSize = 0.5;
boardOffset = 0.10;
boardAngle = 20;

difference() {
  translate([0, 0, smallGridSize/4])
    cube(size=[smallGridSize, smallGridSize, smallGridSize/2], center=true);
  translate([0, 0, boardOffset]) rotate([boardAngle, 0, 0]) translate([0, 0, smallGridSize/2])
    cube(size=[smallGridSize*2, smallGridSize*2, smallGridSize], center=true);
}