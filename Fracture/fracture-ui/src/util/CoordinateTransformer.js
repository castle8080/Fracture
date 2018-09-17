
export default class CoordinateTransformer {

  constructor(
      externalCenterX, externalCenterY,
      localCenterX, localCenterY,
      xRatio, yRatio)
  {
    this.externalCenterX = externalCenterX;
    this.externalCenterY = externalCenterY;
    this.localCenterX = localCenterX;
    this.localCenterY = localCenterY;
    this.xRatio = xRatio;
    this.yRatio = yRatio;
  }

  toLocal(x, y) {
    var localX = (x - this.externalCenterX) * this.xRatio + this.localCenterX;
    var localY = (y - this.externalCenterY) * this.yRatio + this.localCenterY;
    return [localX, localY];
  }

  toExternal(x, y) {
    var externalX = (x - this.localCenterX) / this.xRatio + this.externalCenterX;
    var externalY = (y - this.localCenterY) / this.yRatio + this.externalCenterY;
    return [externalX, externalY];
  }

  static createPixelTransformer(
    pixWidth,
    pixHeight,
    logicalXOrigin,
    logicalYOrigin,
    logicalWidth)
  {
    return new CoordinateTransformer(
        pixWidth / 2.0,
        pixHeight / 2.0,
        logicalXOrigin,
        logicalYOrigin,
        logicalWidth / pixWidth,
        -(logicalWidth / pixWidth),
    );
  }
};
