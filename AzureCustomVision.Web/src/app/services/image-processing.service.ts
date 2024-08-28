import { BoundingBox } from '@/models/object-detection.model';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImageProcessingService {

  drawCroppedImage(
    canvas: HTMLCanvasElement,
    img: HTMLImageElement,
    boundingBox: BoundingBox
  ): void {
    const context = canvas.getContext('2d');
    if (!context) return;

    const imgWidth = img.naturalWidth;
    const imgHeight = img.naturalHeight;

    const absX = boundingBox.left * imgWidth;
    const absY = boundingBox.top * imgHeight;
    const absWidth = boundingBox.width * imgWidth;
    const absHeight = boundingBox.height * imgHeight;

    canvas.width = absWidth;
    canvas.height = absHeight;

    context.drawImage(img, absX, absY, absWidth, absHeight, 0, 0, absWidth, absHeight);
  }

  prepareCanvasForUpload(canvas: HTMLCanvasElement): Promise<Blob | null> {
    return new Promise((resolve) => {
      const context = canvas.getContext('2d');
      if (!context) return resolve(null);

      let { width, height } = canvas;

      if (width < 50 || height < 50) {
        const scale = Math.max(50 / width, 50 / height);
        width = Math.floor(width * scale);
        height = Math.floor(height * scale);

        const tempCanvas = document.createElement('canvas');
        const tempContext = tempCanvas.getContext('2d');
        if (!tempContext) return resolve(null);

        tempCanvas.width = width;
        tempCanvas.height = height;

        tempContext.drawImage(canvas, 0, 0, canvas.width, canvas.height, 0, 0, width, height);

        canvas.width = width;
        canvas.height = height;
        context.drawImage(tempCanvas, 0, 0);
      }

      canvas.toBlob((blob) => resolve(blob), 'image/png');
    });
  }
}
