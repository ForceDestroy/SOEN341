//Angular Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

//Components
import { HomeComponent } from './home.component';
import { SharedModule } from '../shared/shared.module';

//Material Imports
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/Icon';


const homeRoutes: Routes = [
  {path: '', component: HomeComponent},
];



@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    SharedModule,
    MatIconModule,
    MatToolbarModule,
    RouterModule.forChild(homeRoutes),
  ]
})
export class HomeModule { }
