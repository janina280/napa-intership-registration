import {Component, HostListener, OnInit} from '@angular/core';
import { ShipService } from '../../services/ship.service';
import { PortService } from '../../services/port.service';
import { VoyageService } from '../../services/voyage.service';
import { Chart, registerables } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-dashboard',
  imports: [
    BaseChartDirective
  ],
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  shipLabels: string[] = [];
  shipData: number[] = [];

  portLabels: string[] = [];
  portData: number[] = [];

  voyageLabels: string[] = [];
  voyageData: number[] = [];

  constructor(
    private shipService: ShipService,
    private portService: PortService,
    private voyageService: VoyageService
  ) {
    Chart.register(...registerables);
  }

  ngOnInit(): void {
    this.shipService.getAll().subscribe(ships => {
      this.shipLabels = ships.map(s => s.name);
      this.shipData = ships.map(s => s.maximumSpeed);
      console.log('Ship Data:', this.shipData);
    });

    this.portService.getAll().subscribe(ports => {
      const countries = ports.map(p => p.country);
      this.portLabels = [...new Set(countries)];
      this.portData = this.portLabels.map(country =>
        ports.filter(p => p.country === country).length
      );
    });

    this.voyageService.getAll().subscribe(voyages => {
      const dates = voyages.map(v => v.voyageDate?.split('T')[0]);
      this.voyageLabels = [...new Set(dates)];
      this.voyageData = this.voyageLabels.map(date =>
        voyages.filter(v => v.voyageDate?.startsWith(date)).length
      );
    });
  }

  shipChartOptions = {
    responsive: true
  };

  portChartOptions = {
    responsive: true
  };

  voyageChartOptions = {
    responsive: true
  };
}
