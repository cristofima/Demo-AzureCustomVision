import { ImageAnalysis } from '@/models/image-analysis.model';
import { ApiService } from '@/services/api.service';
import { ImageSelectionComponent } from '@/shared/components/image-selection/image-selection.component';
import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-image-analysis',
  standalone: true,
  imports: [CommonModule, ImageSelectionComponent, ButtonModule],
  templateUrl: './image-analysis.component.html'
})
export class ImageAnalysisComponent {

  selectedFile: File | null = null;
  selectedFileURL: string | null = null;
  isLoading: boolean = false;
  response?: ImageAnalysis;

  @ViewChild('uploadedImage', { static: false }) uploadedImageRef!: ElementRef<HTMLImageElement>;

  constructor(private apiService: ApiService) { }

  onUpload() {
    if (!this.selectedFile) return;

    this.isLoading = true;
    this.response = undefined;
    this.selectedFileURL = URL.createObjectURL(this.selectedFile);
    this.apiService.analyzeImage(this.selectedFile).subscribe(response => {
      this.response = response;
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
    });
  }
}
