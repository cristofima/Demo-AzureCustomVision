import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { TabMenuModule } from 'primeng/tabmenu';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TabMenuModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {

  items: MenuItem[] = [];

  ngOnInit(): void {
    this.items = [
      {
        label: 'Image Classification',
        routerLink: ['/image-classification']
      },
      {
        label: 'Object Detection',
        routerLink: ['/object-detection']
      },
      {
        label: 'Image Analysis',
        routerLink: ['/image-analysis']
      },
    ];
  }
}
