//Angular Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

//Components
import { HomeComponent } from './home.component';
import { SharedModule } from '../shared/shared.module';

//Material Imports
import { MatDividerModule } from '@angular/material/divider';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/Icon';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatButtonModule } from '@angular/material/button';


const homeRoutes: Routes = [
  {path: ':id', component: HomeComponent},
];



@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    SharedModule,
    MatDividerModule,
    MatIconModule,
    MatCardModule,
    MatGridListModule,
    MatButtonModule,
    MatToolbarModule,
    RouterModule.forChild(homeRoutes),
  ]
})
export class HomeModule { }
