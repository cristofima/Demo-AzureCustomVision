import { CommonModule } from '@angular/common';
import { ApiService } from '@/services/api.service';
import { Component, ElementRef, NgZone, ViewChild } from '@angular/core';
import { ObjectDetection } from '@/models/object-detection.model';
import { ButtonModule } from 'primeng/button';
import { ImageSelectionComponent } from '@/shared/components/image-selection/image-selection.component';
import { ImageProcessingService } from '@/services/image-processing.service';

@Component({
  selector: 'app-object-detection',
  standalone: true,
  imports: [CommonModule, ButtonModule, ImageSelectionComponent],
  templateUrl: './object-detection.component.html'
})
export class ObjectDetectionComponent {

  selectedFile: File | null = null;
  selectedFileURL: string | null = null;
  plateNumber: string = '';
  isLoading: boolean = false;

  @ViewChild('uploadedImage', { static: false }) uploadedImageRef!: ElementRef<HTMLImageElement>;
  @ViewChild('canvas', { static: false }) canvasRef!: ElementRef<HTMLCanvasElement>;

  constructor(
    private apiService: ApiService,
    private imageProcessingService: ImageProcessingService,
    private ngZone: NgZone) { }

  onUpload() {
    if (!this.selectedFile) return;

    this.isLoading = true;
    this.plateNumber = '';
    if (this.canvasRef?.nativeElement) this.canvasRef.nativeElement.getContext('2d')?.clearRect(0, 0, this.canvasRef.nativeElement.width, this.canvasRef.nativeElement.height);
    this.selectedFileURL = URL.createObjectURL(this.selectedFile);
    this.apiService.detectCarPlate(this.selectedFile).subscribe(response => {
      this.handleDetectionResponse(response);
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
    });
  }

  private handleDetectionResponse(response: ObjectDetection[]): void {
    if (!response[0]) return;

    const boundingBox = response[0].boundingBox;
    const img = this.uploadedImageRef.nativeElement;
    const canvas = this.canvasRef.nativeElement;

    this.imageProcessingService.drawCroppedImage(canvas, img, boundingBox);
    this.sendCroppedImage();
  }

  private sendCroppedImage(): void {
    const canvas = this.canvasRef.nativeElement;

    this.imageProcessingService.prepareCanvasForUpload(canvas).then((blob) => {
      if (blob) {
        this.ngZone.run(() => {
          this.apiService.analyzeImage(blob, 'Read').subscribe((response) => {
            if (response?.read) {
              this.plateNumber = response.read.blocks[0].lines[0].text;
            }
          });
        });
      }
    });
  }
}
