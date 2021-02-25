//Angular Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule, Routes } from '@angular/router';

//Components
import { NewpostComponent } from './newpost/newpost.component';
import { PostViewComponent } from './post-view/post-view.component';
import { SharedModule } from '../shared/shared.module';

//Material Imports
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';


const postRoutes: Routes = [
  {path: 'new', component: NewpostComponent},
  {path: ':id', component: PostViewComponent},
];

@NgModule({
  declarations: [NewpostComponent, PostViewComponent],
  imports: [
    CommonModule,
    SharedModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatButtonModule,
    RouterModule.forChild(postRoutes),
  ]
})
export class PostModule { }
