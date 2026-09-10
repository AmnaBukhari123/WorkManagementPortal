// src/Web/src/app/layout/layout.component.ts
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {
  ContainerComponent,
  SidebarComponent as CoreSidebarComponent,
  SidebarNavComponent,
  SidebarBrandComponent,
  SidebarToggleDirective,
  HeaderComponent,
  HeaderTogglerDirective
} from '@coreui/angular';
import { IconDirective } from '@coreui/icons-angular';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    RouterOutlet,
    ContainerComponent,
    CoreSidebarComponent,
    SidebarNavComponent,
    SidebarBrandComponent,
    SidebarToggleDirective,
    HeaderComponent,
    HeaderTogglerDirective,
    IconDirective
  ],
  template: `
    <c-sidebar class="border-end" narrow visible>
      <c-sidebar-brand>Work Portal</c-sidebar-brand>
      <c-sidebar-nav>
        <a class="nav-link" routerLink="/dashboard">Dashboard</a>
        <a class="nav-link" routerLink="/projects">Projects</a>
      </c-sidebar-nav>
    </c-sidebar>
    <div class="wrapper d-flex flex-column min-vh-100">
      <c-header class="mb-4 d-print-none header header-sticky">
        <c-container class="justify-content-end" fluid>
          <button class="btn btn-outline-secondary" (click)="logout()">Logout</button>
        </c-container>
      </c-header>
      <div class="body flex-grow-1 px-3">
        <router-outlet></router-outlet>
      </div>
    </div>
  `
})
export class LayoutComponent {
  logout() {
    // wired up once AuthService exists
  }
}