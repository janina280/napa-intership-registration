import {Component, OnInit} from '@angular/core';
import {Voyage} from '../../models/voyage';
import {VoyageService} from '../../services/voyage.service';
import {DatePipe, NgForOf} from '@angular/common';

@Component({
  selector: 'app-voyages',
  imports: [
    NgForOf,
    DatePipe
  ],
  templateUrl: './voyages.component.html',
  styleUrl: './voyages.component.css'
})
export class VoyagesComponent implements OnInit{
  voyages: Voyage[] = [];

  constructor(private voyageService: VoyageService) {}

  ngOnInit(): void {
    this.voyageService.getAll().subscribe(data => {
      this.voyages = data;
    });
  }
}
