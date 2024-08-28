import { CommonModule } from '@angular/common';
import { Component, model, ViewChild } from '@angular/core';
import { FileUpload, FileUploadModule } from 'primeng/fileupload';

@Component({
  selector: 'app-image-selection',
  standalone: true,
  imports: [CommonModule, FileUploadModule],
  templateUrl: './image-selection.component.html',
  styleUrl: './image-selection.component.scss'
})
export class ImageSelectionComponent {

  selectedFile = model<File | null>(null);
  @ViewChild('imageFileUpload', { static: false }) imageFileUpload?: FileUpload;

  onFileSelected(event: any) {
    this.selectedFile.set(event.files[0])
  }

  onRemoveFile() {
    this.selectedFile.set(null);
  }
}
