import { Prediction } from '@/models/prediction.model';
import { ApiService } from '@/services/api.service';
import { ImageSelectionComponent } from '@/shared/components/image-selection/image-selection.component';
import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-image-classification',
  standalone: true,
  imports: [CommonModule, ButtonModule, ImageSelectionComponent],
  templateUrl: './image-classification.component.html'
})
export class ImageClassificationComponent {

  selectedFile: File | null = null;
  selectedFileURL: string | null = null;
  isLoading: boolean = false;
  predictions: Prediction[] = [];

  @ViewChild('uploadedImage', { static: false }) uploadedImageRef!: ElementRef<HTMLImageElement>;

  constructor(private apiService: ApiService) { }

  onUpload() {
    if (!this.selectedFile) return;

    this.isLoading = true;
    this.predictions = [];
    this.selectedFileURL = URL.createObjectURL(this.selectedFile);
    this.apiService.classifyPlantDisease(this.selectedFile).subscribe(response => {
      this.predictions = response;
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
    });
  }
}
